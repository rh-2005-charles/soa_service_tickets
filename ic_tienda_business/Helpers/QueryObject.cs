using System.Runtime.Serialization;

namespace ic_tienda_business.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "id";
        public bool IsDescending { get; set; } = false;
    }

    public class TicketTypeQueryObject : QueryObject
    {
        public int? EventId { get; set; }
    }
}
