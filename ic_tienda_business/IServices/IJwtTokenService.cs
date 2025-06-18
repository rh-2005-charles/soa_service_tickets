using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(CustomerResponse customer);
        ClaimsPrincipal ValidateToken(string token);
    }
}