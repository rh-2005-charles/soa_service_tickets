using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "id";
        public bool IsDecsending { get; set; } = false;
    }

    public class CardQueryObject : QueryObject
    {
        public int CardQueryId { get; set; }
        public int? CategoryQueryId { get; set; } = null;
        public bool OnlyDiscounted { get; set; } = false;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
    }

    public class QueryPagined
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class QueryObjectCustomer : QueryObject
    {
        public int CustomerId { get; set; }
    }

    public class QueryObjectOrder : QueryObject
    {
        public string? Status { get; set; } = null;
        public List<int>? Ids { get; set; }
    }

    public class QueryObjectOrderTwo : QueryObject
    {
        public int CustomerId { get; set; }
        public string? State { get; set; } = null;
    }

    public class QueryObjectReceipt : QueryObject
    {
      //  public int? CustomerId { get; set; } = null;
        public string? Type { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        // public string? CustomerName { get; set; } = null;
    }
}
