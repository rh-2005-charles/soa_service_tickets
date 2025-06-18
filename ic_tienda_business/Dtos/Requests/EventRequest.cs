using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class EventRequest
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Description { get; set; }
        [DataMember(Order = 3)]
        public DateTime Date { get; set; }
        [DataMember(Order = 4)]
        public string Location { get; set; }
        [DataMember(Order = 5)]
        public string ImageUrl { get; set; }
        [DataMember(Order = 6)]
        public string Status { get; set; }
    }
}