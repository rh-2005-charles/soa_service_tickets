using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Range(0, 1000000), Precision(10, 2)]
        public decimal Price { get; set; }

        [Required, Range(0, 100000)]
        public int Quantity { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}