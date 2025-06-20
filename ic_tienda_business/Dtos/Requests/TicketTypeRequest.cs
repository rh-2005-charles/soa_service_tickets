using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketTypeRequest
    {
        [DataMember(Order = 1)]
        public int EventId { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public decimal Price { get; set; }

        [DataMember(Order = 4)]
        public int Quantity { get; set; }

        [DataMember(Order = 5)]
        public string Description { get; set; }
    }
}