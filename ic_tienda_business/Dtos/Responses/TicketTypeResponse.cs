using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    public class TicketTypeResponse
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}