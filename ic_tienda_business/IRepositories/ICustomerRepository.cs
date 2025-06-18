using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;

namespace ic_tienda_business.IRepositories
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse?> GetByEmail(string email);
        Task<CustomerAuthResponse> Create(CustomerRegisterRequest customer);
    }
}