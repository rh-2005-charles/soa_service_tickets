using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }


        [Required, Range(0, 1000000)]
        public int Quantity { get; set; }

        [Required, Range(0, 1000000), Precision(10, 2)]
        public decimal SubTotal { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}