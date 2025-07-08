using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class TicketTypeRequest
    {
        public int EventId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}