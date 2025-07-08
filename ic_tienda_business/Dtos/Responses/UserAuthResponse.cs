using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    public class UserAuthResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        public DateTime TokenExpiration { get; set; }
    }


    public class UserResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string? Phone { get; set; }
        
        public string? Role { get; set; }
    }
}