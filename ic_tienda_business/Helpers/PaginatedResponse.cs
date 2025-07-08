using System.Runtime.Serialization;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.Helpers
{
    public class PaginatedResponse<T>
    {
        public List<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

}
