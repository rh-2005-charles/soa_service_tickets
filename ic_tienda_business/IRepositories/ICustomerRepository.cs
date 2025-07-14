using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IRepositories
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse?> GetByEmail(string email);
        Task<CustomerAuthResponse> Create(CustomerRegisterRequest customer);

        Task<CustomerResponse> GetById(int id);

        // Cancelar Evento
        Task<bool> CancelOrderAsync(int orderId);
    }
}