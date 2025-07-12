using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices.Images;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using ic_tienda_utils.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IcTiendaDbContext _context;
        private readonly IImageService _image;
        public EventRepository(IcTiendaDbContext context, IImageService image)
        {
            _context = context;
            _image = image;
        }

        public async Task<EventResponse> AddAsync(EventRequest eventRequest)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(eventRequest.Name))
                errors.Add("El nombre del evento es requerido.");

            if (await _context.Events.AnyAsync(c => c.Name == eventRequest.Name))
                errors.Add("Ya existe un evento con el mismo nombre.");

            if (eventRequest.ImgPath == null || eventRequest.ImgPath.Length == 0)
                errors.Add("La imagen del evento es requerida.");

            if (errors.Any())
                throw new ValidationException(errors);


            var eventEntity = EventMapper.ToEntity(eventRequest);
            eventEntity.ImageUrl = "img_placeholder.png";

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            // Subir la imagen a Cloudinary
            try
            {
#pragma warning disable CS8604
                eventEntity.ImageUrl = await _image.UploadImageAsync(eventRequest.ImgPath, $"evento_{eventEntity.Id}");
#pragma warning restore CS8604

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Si falla la subida de la imagen, eliminar el evento creado
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
                throw new Exception("Error al subir la imagen del evento: " + ex.Message);
            }

            return EventMapper.ToResponse(eventEntity);
        }



        public async Task DeleteAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                throw new KeyNotFoundException("Evento no encontrado.");

            // Eliminar imagen asociada si existe
            if (!string.IsNullOrEmpty(eventEntity.ImageUrl) &&
                !eventEntity.ImageUrl.Equals("temp_url", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var fileName = _image.GetPublicIdFromUrl(eventEntity.ImageUrl);
                    await _image.DeleteImageAsync(fileName);
                }
                catch (Exception ex)
                {
                    // Loggear error pero continuar con la eliminación del evento
                    Console.WriteLine($"Error al eliminar imagen: {ex.Message}");
                }
            }

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query)
        {
            var queryAble = _context.Events.OrderBy(e => e.Id).AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(query.Search))
                queryAble = queryAble.Where(e => e.Name.Contains(query.Search));

            // Apply pagination
            var totalCount = await queryAble.CountAsync();
            var items = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            var responses = items.Select(EventMapper.ToResponse).ToList();

            return new PaginatedResponse<EventResponse>
            {
                Items = responses,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            };

        }

        public async Task<EventResponse> GetByIdAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
#pragma warning disable CS8603
            return eventEntity == null ? null : EventMapper.ToResponse(eventEntity);
#pragma warning restore CS8603
        }

        public async Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest)
        {
            var eventEntity = _context.Events.Find(id);
            if (eventEntity == null)
                throw new KeyNotFoundException("Evento no encontrado.");

            var errors = new List<string>();

            // Validar cambios
            if (string.IsNullOrWhiteSpace(eventRequest.Name))
                errors.Add("El nombre del evento es requerido.");

            if (eventEntity.Name != eventRequest.Name &&
                await _context.Events.AnyAsync(e => e.Name == eventRequest.Name))
                errors.Add("Ya existe un evento con el mismo nombre.");

            if (errors.Any())
                throw new ValidationException(errors);

            // Manejo de la imagen (fuera del mapper)
            if (eventRequest.ImgPath != null && eventRequest.ImgPath.Length > 0)
            {
                // Eliminar imagen anterior si existe
                if (!string.IsNullOrEmpty(eventEntity.ImageUrl) &&
                    !eventEntity.ImageUrl.Equals("temp_url", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var oldFileName = _image.GetPublicIdFromUrl(eventEntity.ImageUrl);
                        await _image.DeleteImageAsync(oldFileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al eliminar imagen anterior: {ex.Message}");
                        // No lanzar excepción para permitir continuar con la actualización
                    }
                }

                // Subir nueva imagen
                eventEntity.ImageUrl = await _image.UploadImageAsync(
                    eventRequest.ImgPath,
                    $"evento_{eventEntity.Id}");
            }

            // Actualizar campos usando el mapper (excepto ImageUrl)
            EventMapper.UpdateEntity(eventEntity, eventRequest);

            await _context.SaveChangesAsync();

            return EventMapper.ToResponse(eventEntity);
        }
    }
}