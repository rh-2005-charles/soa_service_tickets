using System.Security.Claims;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(CustomerResponse customer);
        ClaimsPrincipal ValidateToken(string token);


        string GenerateUserToken(UserResponse user);

    }
}