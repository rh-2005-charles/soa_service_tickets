using System.Runtime.Serialization;

namespace ic_tienda_business.Helpers
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class QueryObject
    {
        [DataMember(Order = 1)]
        public string? Search { get; set; }
        [DataMember(Order = 2)]
        public int PageNumber { get; set; } = 1;
        [DataMember(Order = 3)]
        public int PageSize { get; set; } = 10;
        [DataMember(Order = 4)]
        public string? SortBy { get; set; } = "id";
        [DataMember(Order = 5)]
        public bool IsDescending { get; set; } = false;
    }
}
