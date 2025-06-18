using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Otros
{
    public class NotificationResponse
    {
        public string Target { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public Dictionary<string, string>? Data { get; set; }
    }
}