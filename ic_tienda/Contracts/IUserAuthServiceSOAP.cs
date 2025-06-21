using System.ServiceModel;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]

    public interface IUserAuthServiceSOAP
    {
        [OperationContract]
        UserAuthResponse Login(UserLoginRequest request);

        [OperationContract]
        UserAuthResponse Register(UserRegisterRequest request);

        [OperationContract]
        UserResponse GetById(int id);
    }
}