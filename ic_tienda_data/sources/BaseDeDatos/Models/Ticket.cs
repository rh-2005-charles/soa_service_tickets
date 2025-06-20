using System.ComponentModel.DataAnnotations;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string Status { get; set; }
        public int? SeatNumber { get; set; }

        public string? TicketUrl { get; set; }
        public string? QrCode { get; set; }
    }
}