using System.ComponentModel.DataAnnotations;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }

        public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    }
}