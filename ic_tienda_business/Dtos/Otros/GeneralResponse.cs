using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Otros
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string TitleMessage { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
        public T? Content { get; set; }
    }

    public class GeneralResponse
    {
        public bool Success { get; set; }
        public string TitleMessage { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
    }
}