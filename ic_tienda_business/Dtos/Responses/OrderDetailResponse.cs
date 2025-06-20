using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderDetailResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int OrderId { get; set; }

        [DataMember(Order = 3)]
        public int TicketTypeId { get; set; }

        [DataMember(Order = 4)]
        public int Quantity { get; set; }

        [DataMember(Order = 5)]
        public decimal SubTotal { get; set; }
    }
}