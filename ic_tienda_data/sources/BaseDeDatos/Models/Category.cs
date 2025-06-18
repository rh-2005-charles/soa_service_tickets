using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ic_tienda_data.sources.BaseDeDatos.Models.ActivityLogs;

namespace ic_tienda_data.sources.BaseDeDatos.Models
{
    public class Category 
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("img_url")]
        public string? ImgUrl { get; set; }

       // public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}