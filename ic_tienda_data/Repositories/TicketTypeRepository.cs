using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly IcTiendaDbContext _context;
        public TicketTypeRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<TicketTypeResponse> AddAsync(TicketTypeRequest request)
        {
            bool exists = await _context.TicketTypes.AnyAsync(t => t.Name == request.Name);
            if (exists) throw new InvalidOperationException("Ya existe con ese nombre");

            var ticketMap = TicketTypeMapper.ToEntity(request);

            _context.TicketTypes.Add(ticketMap);
            await _context.SaveChangesAsync();

            return TicketTypeMapper.ToResponse(ticketMap);
        }

        public async Task DeleteAsync(int id)
        {
            var envent = await _context.TicketTypes.FindAsync(id);
            if (envent != null)
            {
                _context.TicketTypes.Remove(envent);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Tipo de ticket no encontrado.");
            }
        }

        public async Task<PaginatedResponse<TicketTypeResponse>> GetAllAsync(QueryObject query)
        {
            var queryAble = _context.TicketTypes.OrderBy(e => e.Id).AsQueryable();

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


            var responses = items.Select(TicketTypeMapper.ToResponse).ToList();


            return new PaginatedResponse<TicketTypeResponse>
            {
                Items = responses,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

        public async Task<List<TicketTypeResponse>> GetByEventIdAsync(int eventId)
        {
            var ticketTypes = await _context.TicketTypes
                .Where(t => t.EventId == eventId)
                .ToListAsync();

            return ticketTypes.Select(TicketTypeMapper.ToResponse).ToList();
        }

        public async Task<TicketTypeResponse> GetByIdAsync(int id)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null)
            {
                throw new KeyNotFoundException($"TicketType con ID {id} no encontrado");
            }

            return TicketTypeMapper.ToResponse(ticketType);
        }

        public async Task<TicketTypeResponse> UpdateAsync(int id, TicketTypeRequest request)
        {
            var ticketType = _context.TicketTypes.Find(id);
            if (ticketType == null)
            {
                throw new KeyNotFoundException($"TicketType con ID {id} no encontrado");
            }

            TicketTypeMapper.UpdateEntity(ticketType, request);
            await _context.SaveChangesAsync();
            return TicketTypeMapper.ToResponse(ticketType);
        }
    }
}