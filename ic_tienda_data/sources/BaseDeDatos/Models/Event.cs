using System.ComponentModel.DataAnnotations;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(200)]
        public string Location { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}