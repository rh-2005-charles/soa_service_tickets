using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? PaymentType { get; set; }
        public string? ShippingType { get; set; }
        public string? ReceiptType { get; set; }
        public decimal? PriceShipping { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string? State { get; set; }
        public string? MessageStatus { get; set; }
        public string? Location { get; set; }
        public string? Reference { get; set; }
        public string? Lat { get; set; }
        public string? Lng { get; set; }
        public string? ImgPath { get; set; }
    }
}