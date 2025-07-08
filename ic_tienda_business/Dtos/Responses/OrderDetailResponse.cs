using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    public class OrderDetailResponse
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int TicketTypeId { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public string? TicketTypeName { get; set; }

        public decimal? UnitPrice { get; set; }

        public List<TicketResponse> Tickets { get; set; }
    }
}