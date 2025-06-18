using CoreWCF;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface ICustomerAuthServiceSOAP
    {
        [OperationContract]
        CustomerAuthResponse Login(CustomerLoginRequest request);

        [OperationContract]
        CustomerAuthResponse Register(CustomerRegisterRequest request);
    }
}