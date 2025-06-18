using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_data.sources.BaseDeDatos.Models.ActivityLogs;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("paymentType")]
        public string? PaymentType { get; set; }

        [Column("shippingType")] //Tipo_Envio
        public string? ShippingType { get; set; }

        [Column("receiptType")] //Tipo_Recibo
        public string? ReceiptType { get; set; }

        [Column("priceShipping")] // Precio_Envio
        [Precision(10, 2)]
        public decimal? PriceShipping { get; set; }

        [Column("totalAmount")]
        [Precision(10, 2)] // Monto_Total
        public decimal TotalAmount { get; set; }

        [Column("customerId")]
        public int CustomerId { get; set; }//ClientId

        [Column("state")]
        public string? State { get; set; }

        [Column("messageStatus")] // Mensage_Estado
        public string? MessageStatus { get; set; }
        //opcional, solo si el tipo de envio es delivery

        [Column("location")]
        public string? Location { get; set; }

        [Column("reference")]
        public string? Reference { get; set; }

        [Column("lat")]
        public string? Lat { get; set; }

        [Column("lng")]
        public string? Lng { get; set; }

        [Column("img_url")]
        public string? ImgUrl { get; set; } //= "default.png";

        // Navigation property for order details
        //  public Customer? Customer { get; set; }
        //  public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        //  public Payment? Payment { get; set; }
        //  public Receipt? Receipt { get; set; }
    }
}