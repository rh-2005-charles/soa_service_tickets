using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string PaymentMethod { get; set; }

        public int TransactionId { get; set; }

        public List<OrderDetailResponse> OrderDetails { get; set; }
    }
}