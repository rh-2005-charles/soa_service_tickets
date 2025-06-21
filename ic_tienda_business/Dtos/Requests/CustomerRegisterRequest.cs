using System.Runtime.Serialization;

namespace ic_tienda_business.Dtos.Requests
{
    [DataContract(Namespace = "http://tempuri.org/")]

    public class CustomerRegisterRequest
    {

        [DataMember(Order = 1)]
        public string FirstName { get; set; }
        [DataMember(Order = 2)]
        public string LastName { get; set; }
        [DataMember(Order = 3)]
        public string Email { get; set; }
        [DataMember(Order = 4)]
        public string Password { get; set; }
        [DataMember(Order = 5)]
        public string? Phone { get; set; }
    }
}