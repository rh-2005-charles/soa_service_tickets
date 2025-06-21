using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int CustomerId { get; set; }

        [DataMember(Order = 3)]
        public DateTime OrderDate { get; set; }

        [DataMember(Order = 4)]
        public decimal TotalAmount { get; set; }

        [DataMember(Order = 5)]
        public string Status { get; set; }

        [DataMember(Order = 6)]
        public string PaymentMethod { get; set; }

        [DataMember(Order = 7)]
        public int TransactionId { get; set; }

        [DataMember(Order = 8)]
        public List<OrderDetailResponse> OrderDetails { get; set; }
    }
}