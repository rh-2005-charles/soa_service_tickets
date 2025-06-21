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

        /* public static OrderResponse ToResponse(Order entity)
        {
            var response = new OrderResponse
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                OrderDate = entity.OrderDate,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status,
                PaymentMethod = entity.PaymentMethod,
                TransactionId = entity.TransactionId,
                OrderDetails = entity.OrderDetails?.Select(od => new OrderDetailResponse
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    TicketTypeId = od.TicketTypeId,
                    TicketTypeName = od.TicketType?.Name ?? string.Empty,
                    Quantity = od.Quantity,
                    UnitPrice = od.TicketType?.Price ?? 0,
                    SubTotal = od.SubTotal,
                    Tickets = od.Tickets?.Select(t => new TicketResponse
                    {
                        Id = t.Id,
                        EventId = t.EventId,
                        TicketTypeId = t.TicketTypeId,
                        Status = t.Status,
                        SeatNumber = t.SeatNumber
                    }).ToList() ?? new List<TicketResponse>()
                }).ToList() ?? new List<OrderDetailResponse>()
            };
            return response;
        }
 */


        public static OrderResponse ToResponse(Order entity)
        {
            var response = new OrderResponse
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                OrderDate = entity.OrderDate,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status,
                PaymentMethod = entity.PaymentMethod,
                TransactionId = entity.TransactionId,
                OrderDetails = new List<OrderDetailResponse>()
            };

            foreach (var detail in entity.OrderDetails)
            {
                var detailResponse = new OrderDetailResponse
                {
                    Id = detail.Id,
                    OrderId = detail.OrderId,
                    TicketTypeId = detail.TicketTypeId,
                    TicketTypeName = detail.TicketType?.Name ?? string.Empty,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.TicketType?.Price ?? 0,
                    SubTotal = detail.SubTotal,
                    Tickets = new List<TicketResponse>()
                };

                foreach (var ticket in detail.Tickets)
                {
                    detailResponse.Tickets.Add(new TicketResponse
                    {
                        Id = ticket.Id,
                        OrderDetailId = ticket.OrderDetailId,
                        EventId = ticket.EventId,
                        EventName = ticket.Event?.Name ?? string.Empty,
                        TicketTypeId = ticket.TicketTypeId,
                        TicketTypeName = ticket.TicketType?.Name ?? string.Empty,
                        CustomerId = ticket.CustomerId,
                        CustomerName = $"{ticket.Customer?.FirstName} {ticket.Customer?.LastName}",
                        Status = ticket.Status,
                        SeatNumber = ticket.SeatNumber,
                        TicketUrl = ticket.TicketUrl,
                        QrCode = ticket.QrCode
                    });
                }

                response.OrderDetails.Add(detailResponse);
            }

            return response;
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