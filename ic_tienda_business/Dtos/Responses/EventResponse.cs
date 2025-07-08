using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    public class EventResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }
        
        public string Status { get; set; }
    }
}