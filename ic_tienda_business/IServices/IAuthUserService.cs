using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IServices
{
    public interface IAuthUserService
    {
        Task<UserAuthResponse> Login(UserLoginRequest request);
        Task<UserAuthResponse> Register(UserRegisterRequest request);
        Task<UserResponse> GetById(int id);
    }
}