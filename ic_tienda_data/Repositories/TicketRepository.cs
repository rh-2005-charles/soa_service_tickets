using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IcTiendaDbContext _context;
        public TicketRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<TicketResponse> AddAsync(TicketRequest request)
        {
            var ticketMap = TicketMapper.ToEntity(request);

            _context.Tickets.Add(ticketMap);
            await _context.SaveChangesAsync();

            return TicketMapper.ToResponse(ticketMap);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Tickets.FindAsync(id);
            if (item != null)
            {
                _context.Tickets.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Item no encontrado.");
            }
        }

        public async Task<PaginatedResponse<TicketResponse>> GetAllAsync(QueryObject query)
        {
            var queryAble = _context.Tickets.OrderBy(e => e.Id).AsQueryable();

            // Apply pagination
            var totalCount = await queryAble.CountAsync();
            var items = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();


            var responses = items.Select(TicketMapper.ToResponse).ToList();


            return new PaginatedResponse<TicketResponse>
            {
                Items = responses,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

        public async Task<TicketResponse> GetByIdAsync(int id)
        {
            var item = await _context.Tickets.FindAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Item con ID {id} no encontrado");
            }

            return TicketMapper.ToResponse(item);
        }

        public async Task<TicketResponse> UpdateAsync(int id, TicketRequest request)
        {
            var item = _context.Tickets.Find(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Item con ID {id} no encontrado");
            }

            TicketMapper.UpdateEntity(item, request);
            await _context.SaveChangesAsync();
            return TicketMapper.ToResponse(item);
        }
    }
}