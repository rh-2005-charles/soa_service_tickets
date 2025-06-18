using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Requests
{
    public class OrderDetailRequest
    {
        //public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Comment { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal SubTotal { get; set; }
        public string? Options { get; set; }
    }
}