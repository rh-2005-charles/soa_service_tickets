using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int OrderDetailId { get; set; }

        [DataMember(Order = 3)]
        public int EventId { get; set; }

        [DataMember(Order = 4)]
        public int TicketTypeId { get; set; }

        [DataMember(Order = 5)]
        public int CustomerId { get; set; }

        [DataMember(Order = 6)]
        public string Status { get; set; }

        [DataMember(Order = 7)]
        public int? SeatNumber { get; set; }

        [DataMember(Order = 8)]
        public string? TicketUrl { get; set; }

        [DataMember(Order = 9)]
        public string? QrCode { get; set; }
    }
}