using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public class OrderDetailMapper
    {
        public static OrderDetail ToEntity(OrderDetailRequest request)
        {
            return new OrderDetail
            {
                OrderId = request.OrderId,
                TicketTypeId = request.TicketTypeId,
                Quantity = request.Quantity,
                SubTotal = request.SubTotal,
            };
        }

        public static OrderDetailResponse ToResponse(OrderDetail entity)
        {
            return new OrderDetailResponse
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                TicketTypeId = entity.TicketTypeId,
                TicketTypeName = entity.TicketType?.Name ?? string.Empty,
                UnitPrice = entity.TicketType?.Price ?? 0,
                Quantity = entity.Quantity,
                SubTotal = entity.SubTotal,
                Tickets = entity.Tickets?.Select(t => new TicketResponse
                {
                    Id = t.Id,
                    EventId = t.EventId,
                    TicketTypeId = t.TicketTypeId,
                    Status = t.Status,
                    SeatNumber = t.SeatNumber
                }).ToList() ?? new List<TicketResponse>()
            };
        }

        public static void UpdateEntity(OrderDetail entity, OrderDetailRequest request)
        {
            entity.OrderId = request.OrderId;
            entity.TicketTypeId = request.TicketTypeId;
            entity.Quantity = request.Quantity;
            entity.SubTotal = request.SubTotal;
        }
    }
}