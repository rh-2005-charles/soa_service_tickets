using System.ComponentModel.DataAnnotations;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}