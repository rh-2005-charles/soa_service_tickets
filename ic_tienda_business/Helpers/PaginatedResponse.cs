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
        // public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize) ; 
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
        public List<EventResponse> Events { get; set; } = new List<EventResponse>();
    }
}
