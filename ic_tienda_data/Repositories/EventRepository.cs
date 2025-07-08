using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IcTiendaDbContext _context;
        public EventRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<EventResponse> AddAsync(EventRequest eventRequest)
        {
            bool exists = await _context.Events.AnyAsync(c => c.Name == eventRequest.Name);
            if (exists) throw new InvalidOperationException("Ya existe un evento con este nombre.");

            var eventMap = EventMapper.ToEntity(eventRequest);
            _context.Events.Add(eventMap);

            await _context.SaveChangesAsync();

            return EventMapper.ToResponse(eventMap);
        }

        

        public async Task DeleteAsync(int id)
        {
            var envent = await _context.Events.FindAsync(id);
            if (envent != null)
            {
                _context.Events.Remove(envent);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Evento no encontrado.");
            }
        }

        public async Task<PaginatedResponse<EventResponse>> GetAllAsync(QueryObject query)
        {

            try
            {

                var queryAble = _context.Events.OrderBy(e => e.Id).AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(query.Search))
                {
                    queryAble = queryAble.Where(e => e.Name.Contains(query.Search));
                }


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
            catch (Exception ex)
            {
                Console.WriteLine($"[Repository] Error en GetAllAsync: {ex}");
                throw;
            }
        }

        public async Task<EventResponse> GetByIdAsync(int id)
        {
            var eventT = await _context.Events.FindAsync(id);
            if (eventT == null)
            {
                return null;
            }

            // return eventT.ToResponse();
            return EventMapper.ToResponse(eventT);
        }

        public async Task<EventResponse> UpdateAsync(int id, EventRequest eventRequest)
        {
            var eventT = _context.Events.Find(id);
            if (eventT == null)
            {
                return null;
            }

            EventMapper.UpdateEntity(eventT, eventRequest);
            await _context.SaveChangesAsync();
            return EventMapper.ToResponse(eventT);
        }
    }
}