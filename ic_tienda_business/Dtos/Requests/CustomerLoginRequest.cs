using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class CustomerLoginRequest
    {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}