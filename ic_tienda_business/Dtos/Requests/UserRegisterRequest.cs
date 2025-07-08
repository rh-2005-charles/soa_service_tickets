using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; }
    }
}