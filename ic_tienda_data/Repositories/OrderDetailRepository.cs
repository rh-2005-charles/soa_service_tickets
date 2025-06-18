using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
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

        public async Task<OrderDetailResponse> AddAsync(OrderDetailRequest orderDetailRequest)
        {
            var orderDetail = orderDetailRequest.ToOrderDetailMap();

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            //orderDetailRequest.Id = orderDetail.Id;

            return orderDetail.OrderDetailResponseMap();
        }

        public async Task<OrderDetailResponse> GetByIdAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            // if (orderDetail == null) return null;

            if (orderDetail == null)
                throw new KeyNotFoundException("Detalle de la opcion no encontrada.");

            return new OrderDetailResponse
            {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductName = orderDetail.ProductName,
                ProductId = orderDetail.ProductId,
                Comment = orderDetail.Comment,
                ProductQuantity = orderDetail.ProductQuantity,
                ProductPrice = orderDetail.ProductPrice,
                SubTotal = orderDetail.SubTotal,
                Options = orderDetail.Options
            };
        }

        public async Task<IEnumerable<OrderDetailResponse>> GetByOrderIdDetailAsync(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            return orderDetails.Select(od => new OrderDetailResponse
            {
                Id = od.Id,
                OrderId = od.OrderId,
                ProductName = od.ProductName,
                ProductId = od.ProductId,
                Comment = od.Comment,
                ProductQuantity = od.ProductQuantity,
                ProductPrice = od.ProductPrice,
                SubTotal = od.SubTotal,
                Options = od.Options
            });
        }
    }
}