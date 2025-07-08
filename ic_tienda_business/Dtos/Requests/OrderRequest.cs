using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string PaymentMethod { get; set; }

        public int TransactionId { get; set; }

        public List<OrderItemRequest> Items { get; set; }
    }

    public class OrderItemRequest
    {
        public int TicketTypeId { get; set; }

        public int Quantity { get; set; }
    }
}