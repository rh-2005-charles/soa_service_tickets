using System.Runtime.Serialization;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.Helpers
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public abstract class PaginationBase
    {
        [DataMember(Order = 2)]
        public int TotalCount { get; set; }

        [DataMember(Order = 3)]
        public int PageNumber { get; set; }

        [DataMember(Order = 4)]
        public int PageSize { get; set; }

        [DataMember(Order = 5)]
        public int TotalPages { get; set; }
    }


    [DataContract(Namespace = "http://tempuri.org/")]
    public class PaginatedResponse<T> : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<T> Items { get; set; } = new List<T>();
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class EventPaginatedResponse : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<EventResponse> Items { get; set; } = new List<EventResponse>();
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketTypePaginatedResponse : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<TicketTypeResponse> Items { get; set; } = new List<TicketTypeResponse>();
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderPaginatedResponse : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<OrderResponse> Items { get; set; } = new List<OrderResponse>();
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class OrderDetailPaginatedResponse : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<OrderDetailResponse> Items { get; set; } = new List<OrderDetailResponse>();
    }

    [DataContract(Namespace = "http://tempuri.org/")]
    public class TicketPaginatedResponse : PaginationBase
    {
        [DataMember(Order = 1)]
        public List<TicketResponse> Items { get; set; } = new List<TicketResponse>();
    }
}
