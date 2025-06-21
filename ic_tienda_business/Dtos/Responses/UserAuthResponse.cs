using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Responses
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class UserAuthResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Email { get; set; }

        [DataMember(Order = 3)]
        public string FirstName { get; set; }

        [DataMember(Order = 4)]
        public string LastName { get; set; }

        [DataMember(Order = 5)]
        public string Token { get; set; }

        [DataMember(Order = 6)]
        public string Role { get; set; }

        [DataMember(Order = 7)]
        public DateTime TokenExpiration { get; set; }
    }


    [DataContract(Namespace = "http://tempuri.org/")]
    public class UserResponse
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string Email { get; set; }

        [DataMember(Order = 5)]
        public string Password { get; set; }

        [DataMember(Order = 6)]
        public string? Phone { get; set; }
        
        [DataMember(Order = 7)]
        public string? Role { get; set; }
    }
}