using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
#pragma warning disable CS8602
    public class OrderRepository : IOrderRepository
    {
        private readonly IcTiendaDbContext _context;
        //private readonly ICustomerRepository _customerRepository;
        //private readonly EmailOrderService _emailOrderService;
        //public OrderRepository(IcTiendaDbContext context, ICustomerRepository customerRepository, EmailOrderService emailOrderService)
        public OrderRepository(IcTiendaDbContext context)
        {
            _context = context;
            // _customerRepository = customerRepository;
            // _emailOrderService = emailOrderService;
        }

        public async Task<OrderResponse> AddAsync(OrderRequest orderRequest)
        {
            var order = OrderMapper.ToEntity(orderRequest);

            // Add and save to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return OrderMapper.OrderResponseMap(order);
            //orderRequest.Id = order.Id; // Set the ID of the created order
            //return orderRequest;
        }

        public async Task DeleteAsync(OrderResponse orderResponse)
        {
            var order = await _context.Orders.FindAsync(orderResponse.Id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResponse<OrderResponse>> GetAllAsync(QueryObjectOrder query)
        {
            var queryAble = _context.Orders.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryAble = queryAble.Where(o => o.PaymentType.Contains(query.Search) || o.ShippingType.Contains(query.Search));
            }

            if (!string.IsNullOrEmpty(query.Status))
            {
                queryAble = queryAble.Where(o => o.State == query.Status);
            }

            var totalCount = await queryAble.CountAsync();

            // Apply sorting
            queryAble = query.SortBy switch
            {
                "Id" => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id),
                "Date" => query.IsDecsending ? queryAble.OrderBy(o => o.Date) : queryAble.OrderByDescending(o => o.Date),
                _ => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id)
            };

            var orders = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(o => o.OrderResponseMap()) // Reutiliza el método ToDto() del OrderMapper
                .ToListAsync();

            return new PaginatedResponse<OrderResponse>
            {
                Items = orders,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<PaginatedResponse<OrderResponse>> GetAllInProcessAsync(QueryObjectOrder query)
        {
            var queryAble = _context.Orders.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(query.Search))
            {
                queryAble = queryAble.Where(o => o.PaymentType.Contains(query.Search) || o.ShippingType.Contains(query.Search));
            }

            // Apply status filter if provided
            if (!string.IsNullOrEmpty(query.Status))
            {
                // If a status is provided, filter by it
                queryAble = queryAble.Where(o => o.State == query.Status);
            }
            else
            {
                // If no status is provided, exclude "Finalizado" and "Cancelado"
                queryAble = queryAble.Where(o => o.State != "Finalizado" && o.State != "Cancelado");
            }

            var totalCount = await queryAble.CountAsync();

            // Apply sorting
            queryAble = query.SortBy switch
            {
                "id" => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id),
                "Fecha" => query.IsDecsending ? queryAble.OrderBy(o => o.Date) : queryAble.OrderByDescending(o => o.Date),
                _ => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id)
            };

            var orders = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(o => o.OrderResponseMap()) // Reutiliza el método ToDto() del OrderMapper
                .ToListAsync();

            return new PaginatedResponse<OrderResponse>
            {
                Items = orders,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) throw new KeyNotFoundException($"El pedido con ID {id} no fue encontrado.");

            return order.OrderResponseMap();
        }

        public async Task<IEnumerable<OrderResponse>> GetInProcessOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId && o.State != "finalizado" && o.State != "cancelado")
                .Select(o => o.OrderResponseMap())
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<OrderDetailResponse>> GetOrderDetailsAsync(int orderId)
        {
            var orderDetail = await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Select(od => od.OrderDetailResponseMap())
                .ToListAsync();

            return orderDetail;
        }

        public async Task<PaginatedResponse<OrderResponse>> GetOrdersByStatusAsync(QueryObjectOrderTwo query)
        {
            var queryAble = _context.Orders.AsQueryable()
                .Where(o => o.CustomerId == query.CustomerId && o.State == query.State);

            if (!string.IsNullOrEmpty(query.Search))
            {
                queryAble = queryAble.Where(o => o.PaymentType.Contains(query.Search) || o.ShippingType.Contains(query.Search));
            }

            var totalCount = await queryAble.CountAsync();

            queryAble = query.SortBy switch
            {
                "id" => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id),
                "Fecha" => query.IsDecsending ? queryAble.OrderBy(o => o.Date) : queryAble.OrderByDescending(o => o.Date),
                _ => query.IsDecsending ? queryAble.OrderBy(o => o.Id) : queryAble.OrderByDescending(o => o.Id)
            };

            var orders = await queryAble
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(o => o.OrderResponseMap()) // Reutiliza el método ToDto() del OrderMapper
                .ToListAsync();

            return new PaginatedResponse<OrderResponse>
            {
                Items = orders,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<OrderResponse> UpdateAsync(int id, OrderRequest orderRequest)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null) throw new KeyNotFoundException("Order no encontrado.");

            OrderMapper.UpdateEntity(order, orderRequest);

            await _context.SaveChangesAsync();

            return order.OrderResponseMap();
        }
    }

#pragma warning restore CS8602

}