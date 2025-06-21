using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderRequest
    {
        [DataMember(Order = 1)]
        public int CustomerId { get; set; }

        [DataMember(Order = 2)]
        public DateTime OrderDate { get; set; }

        [DataMember(Order = 3)]
        public decimal TotalAmount { get; set; }

        [DataMember(Order = 4)]
        public string Status { get; set; }

        [DataMember(Order = 5)]
        public string PaymentMethod { get; set; }

        [DataMember(Order = 6)]
        public int TransactionId { get; set; }

        [DataMember(Order = 7)]
        public List<OrderItemRequest> Items { get; set; }
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderItemRequest
    {
        [DataMember(Order = 1)]
        public int TicketTypeId { get; set; }

        [DataMember(Order = 2)]
        public int Quantity { get; set; }
    }
}