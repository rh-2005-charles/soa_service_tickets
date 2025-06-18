using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class CustomerLoginRequest
    {

        [DataMember(Order = 1)]
        public string Email { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }
    }
}