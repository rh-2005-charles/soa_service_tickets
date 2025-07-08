using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class OrderDetailRequest
    {
        public int OrderId { get; set; }

        
        public int TicketTypeId { get; set; }


        public int Quantity { get; set; }
        

        public decimal SubTotal { get; set; }
    }
}