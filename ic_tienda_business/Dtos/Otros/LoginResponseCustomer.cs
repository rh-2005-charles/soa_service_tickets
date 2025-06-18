using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Otros
{
    public class LoginResponseCustomer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}