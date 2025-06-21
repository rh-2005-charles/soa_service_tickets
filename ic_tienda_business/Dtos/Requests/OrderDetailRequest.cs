using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderDetailRequest
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        
        [DataMember(Order = 2)]
        public int TicketTypeId { get; set; }


        [DataMember(Order = 3)]
        public int Quantity { get; set; }
        

        [DataMember(Order = 4)]
        public decimal SubTotal { get; set; }
    }
}