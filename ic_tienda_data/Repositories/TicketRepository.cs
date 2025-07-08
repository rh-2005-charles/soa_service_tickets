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

            // Recargar la entidad con sus relaciones
            var ticketWithRelations = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.TicketType)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == ticketMap.Id);

            return TicketMapper.ToResponse(ticketWithRelations);
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
            };
        }

        public async Task<List<TicketResponse>> GetByEventIdAsync(int eventId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.EventId == eventId)
                .ToListAsync();

            return tickets.Select(TicketMapper.ToResponse).ToList();
        }

        public async Task<List<TicketResponse>> GetByIdAsync(int id)
        {

            var tickets = await _context.Orders
                .Where(o => o.Id == id)
                .SelectMany(o => o.OrderDetails)
                .SelectMany(od => od.Tickets)
                .Include(t => t.Event)
                .Include(t => t.TicketType)
                .Include(t => t.Customer)
                .Select(t => new TicketResponse
                {
                    Id = t.Id,
                    OrderDetailId = t.OrderDetailId,
                    EventId = t.EventId,
                    EventName = t.Event.Name,
                    TicketTypeId = t.TicketTypeId,
                    TicketTypeName = t.TicketType.Name,
                    CustomerId = t.CustomerId,
                    CustomerName = $"{t.Customer.FirstName} {t.Customer.LastName}",
                    Status = t.Status,
                    SeatNumber = t.SeatNumber,
                    TicketUrl = t.TicketUrl,
                    QrCode = t.QrCode
                })
               .ToListAsync();

            return tickets;
        }

        public async Task<List<TicketResponse>> GetByOrderDetailIdAsync(int orderDetailId)
        {
            var tickets = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.TicketType)
                .Include(t => t.Customer)
                .Where(t => t.OrderDetailId == orderDetailId)
                .ToListAsync();

            return tickets.Select(TicketMapper.ToResponse).ToList();
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