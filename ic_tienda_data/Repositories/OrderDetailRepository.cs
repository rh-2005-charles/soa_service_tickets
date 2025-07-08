using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IcTiendaDbContext _context;
        public OrderDetailRepository(IcTiendaDbContext context)
        {
            _context = context;

        }

        public async Task<OrderDetailResponse> AddAsync(OrderDetailRequest request)
        {
            // Obtener el TicketType relacionado
            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(tt => tt.Id == request.TicketTypeId);

            if (ticketType == null)
                throw new KeyNotFoundException($"TicketType con ID {request.TicketTypeId} no encontrado");


            var item = OrderDetailMapper.ToEntity(request);

            // Asignar el nombre del TicketType
            item.TicketTypeName = ticketType.Name;

            _context.OrderDetails.Add(item);

            await _context.SaveChangesAsync();

            return OrderDetailMapper.ToResponse(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.OrderDetails.FindAsync(id);
            if (item != null)
            {
                _context.OrderDetails.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Tipo de ticket no encontrado.");
            }
        }

        public async Task<PaginatedResponse<OrderDetailResponse>> GetAllAsync(QueryObject query)
        {
            var queryAble = _context.OrderDetails.OrderBy(e => e.Id).AsQueryable();

            // Apply pagination
            var totalCount = await queryAble.CountAsync();
            var items = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();


            var responses = items.Select(OrderDetailMapper.ToResponse).ToList();


            return new PaginatedResponse<OrderDetailResponse>
            {
                Items = responses,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            };
        }

        public async Task<OrderDetailResponse> GetByIdAsync(int id)
        {
            var item = await _context.OrderDetails.FindAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Order Detail con ID {id} no encontrado");
            }

            return OrderDetailMapper.ToResponse(item);
        }

        public async Task<List<OrderDetailResponse>> GetByOrderIdAsync(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.TicketType)
                .Include(od => od.Tickets)
                    .ThenInclude(t => t.Event)
                .Include(od => od.Tickets)
                    .ThenInclude(t => t.Customer)
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            return orderDetails.Select(OrderDetailMapper.ToResponse).ToList();
        }

        public async Task<OrderDetailResponse> UpdateAsync(int id, OrderDetailRequest request)
        {
            var item = _context.OrderDetails.Find(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Order con ID {id} no encontrado");
            }

            OrderDetailMapper.UpdateEntity(item, request);
            await _context.SaveChangesAsync();
            return OrderDetailMapper.ToResponse(item);
        }
    }
}