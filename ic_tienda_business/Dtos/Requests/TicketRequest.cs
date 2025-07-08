using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class TicketRequest
    {
        public int OrderDetailId { get; set; }

        public int EventId { get; set; }

        public int TicketTypeId { get; set; }

        public int CustomerId { get; set; }

        public string Status { get; set; }

        public int? SeatNumber { get; set; }

        public string? TicketUrl { get; set; }

        public string? QrCode { get; set; }
    }
}