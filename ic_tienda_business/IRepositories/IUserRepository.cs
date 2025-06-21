using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IRepositories
{
    public interface IUserRepository
    {
        Task<UserResponse?> GetByEmail(string email);
        Task<UserAuthResponse> Create(UserRegisterRequest user);

        Task<UserResponse> GetById(int id);
    }
}