using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class EventResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(Order = 4)]
        public DateTime Date { get; set; }

        [DataMember(Order = 5)]
        public string Location { get; set; }

        [DataMember(Order = 6)]
        public string ImageUrl { get; set; }
        
        [DataMember(Order = 7)]
        public string Status { get; set; }
    }
}