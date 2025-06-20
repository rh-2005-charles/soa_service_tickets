using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketTypeResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int EventId { get; set; }

        [DataMember(Order = 3)]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        public decimal Price { get; set; }

        [DataMember(Order = 5)]
        public int Quantity { get; set; }

        [DataMember(Order = 6)]
        public string Description { get; set; }
    }
}