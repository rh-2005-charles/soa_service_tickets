using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketRequest
    {
        [DataMember(Order = 1)]
        public int OrderDetailId { get; set; }

        [DataMember(Order = 2)]
        public int EventId { get; set; }

        [DataMember(Order = 3)]
        public int TicketTypeId { get; set; }

        [DataMember(Order = 4)]
        public int CustomerId { get; set; }

        [DataMember(Order = 5)]
        public string Status { get; set; }

        [DataMember(Order = 6)]
        public int? SeatNumber { get; set; }

        [DataMember(Order = 7)]
        public string? TicketUrl { get; set; }

        [DataMember(Order = 8)]
        public string? QrCode { get; set; }
    }
}