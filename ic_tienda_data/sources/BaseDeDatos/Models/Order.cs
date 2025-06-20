using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, Range(0, 1000000), Precision(10, 2)]
        public decimal TotalAmount { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        public int TransactionId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


    }
}