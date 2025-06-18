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
    [Table("orderDetail")]
    public class OrderDetail //: AuditModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orderId")]
        public int OrderId { get; set; }

        [Column("productId")]
        public int ProductId { get; set; }

        [Column("productName")]
        public string? ProductName { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("productQuantity")]
        public int ProductQuantity { get; set; }

        [Column("productPrice")]
        [Precision(10, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        public decimal ProductPrice { get; set; }

        [Column("subTotal")]
        [Precision(10, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        public decimal SubTotal { get; set; }

        [Column("options")]
        public string? Options { get; set; } 

        //public Order? Order { get; set; } // Navigation property
        //
        //public Product? Product { get; set; } // Navigation property
    }
}