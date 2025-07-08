using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IcTiendaDbContext _context;
        public OrderRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> AddAsync(OrderRequest request)
        {

            var orderMap = OrderMapper.ToEntity(request);
            _context.Orders.Add(orderMap);

            await _context.SaveChangesAsync();

            // Recargar la entidad con sus relaciones
            var orderWithDetails = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.TicketType)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tickets)
                .FirstOrDefaultAsync(o => o.Id == orderMap.Id);

            return OrderMapper.ToResponse(orderWithDetails);
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Tipo de ticket no encontrado.");
            }
        }

        public async Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObject query)
        {
            var queryAble = _context.Orders.OrderBy(e => e.Id).AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryAble = queryAble.Where(e => e.PaymentMethod.Contains(query.Search));
            }


            // Apply pagination
            var totalCount = await queryAble.CountAsync();
            var items = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();


            var responses = items.Select(OrderMapper.ToResponse).ToList();


            return new PaginatedResponse<OrderResponse>
            {
                Items = responses,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            };
        }

        public async Task<List<OrderResponse>> GetByCustomerIdAsync(int customerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(OrderMapper.ToResponse).ToList();
        }



        public async Task<List<OrderResponse>> GetByEventIdAsync(int eventId)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDetails.Any(od =>
                    od.Tickets.Any(t => t.EventId == eventId)))
                .ToListAsync();

            return orders.Select(OrderMapper.ToResponse).ToList();
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _context.Orders
              .AsSplitQuery()
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.TicketType)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tickets)
                        .ThenInclude(t => t.Event)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tickets)
                        .ThenInclude(t => t.TicketType)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Tickets)
                        .ThenInclude(t => t.Customer)
                .AsNoTracking() // Mejorar rendimiento
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order con ID {id} no encontrado");
            }

            return OrderMapper.ToResponse(order);
        }

        public async Task<OrderResponse> UpdateAsync(int id, OrderRequest request)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order con ID {id} no encontrado");
            }

            OrderMapper.UpdateEntity(order, request);
            await _context.SaveChangesAsync();
            return OrderMapper.ToResponse(order);
        }
    }
}