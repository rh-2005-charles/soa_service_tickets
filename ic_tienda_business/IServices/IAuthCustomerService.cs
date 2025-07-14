using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IServices
{
    public interface IAuthCustomerService
    {
        Task<CustomerAuthResponse> Login(CustomerLoginRequest request);
        Task<CustomerAuthResponse> Register(CustomerRegisterRequest request);
        Task<CustomerResponse> GetById(int id);

        Task<bool> CancelOrderAsync(int orderId);

    }
}