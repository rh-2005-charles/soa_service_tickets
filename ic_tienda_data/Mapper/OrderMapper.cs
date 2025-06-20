using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public class OrderMapper
    {
        public static Order ToEntity(OrderRequest request)
        {
            return new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                PaymentMethod = request.PaymentMethod,
                TransactionId = request.TransactionId
            };
        }

        public static OrderResponse ToResponse(Order entity)
        {
            return new OrderResponse
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                OrderDate = entity.OrderDate,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status,
                PaymentMethod = entity.PaymentMethod,
                TransactionId = entity.TransactionId
            };
        }

        public static void UpdateEntity(Order entity, OrderRequest request)
        {
            entity.CustomerId = request.CustomerId;
            entity.OrderDate = request.OrderDate;
            entity.TotalAmount = request.TotalAmount;
            entity.Status = request.Status;
            entity.PaymentMethod = request.PaymentMethod;
            entity.TransactionId = request.TransactionId;
        }
    }
}