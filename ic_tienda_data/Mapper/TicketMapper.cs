using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public class TicketMapper
    {
        public static Ticket ToEntity(TicketRequest request)
        {
            return new Ticket
            {
                OrderDetailId = request.OrderDetailId,
                EventId = request.EventId,
                TicketTypeId = request.TicketTypeId,
                CustomerId = request.CustomerId,
                Status = request.Status,
                SeatNumber = request.SeatNumber,
                TicketUrl = request.TicketUrl,
                QrCode = request.QrCode,
            };
        }

        public static TicketResponse ToResponse(Ticket entity)
        {
            return new TicketResponse
            {
                Id = entity.Id,
                OrderDetailId = entity.OrderDetailId,
                EventId = entity.EventId,
                TicketTypeId = entity.TicketTypeId,
                CustomerId = entity.CustomerId,
                Status = entity.Status,
                SeatNumber = entity.SeatNumber,
                TicketUrl = entity.TicketUrl,
                QrCode = entity.QrCode,
            };
        }

        public static void UpdateEntity(Ticket entity, TicketRequest request)
        {
            entity.OrderDetailId = request.OrderDetailId;
            entity.EventId = request.EventId;
            entity.TicketTypeId = request.TicketTypeId;
            entity.CustomerId = request.CustomerId;
            entity.Status = request.Status;
            entity.SeatNumber = request.SeatNumber;
            entity.TicketUrl = request.TicketUrl;
            entity.QrCode = request.QrCode;
        }
    }
}