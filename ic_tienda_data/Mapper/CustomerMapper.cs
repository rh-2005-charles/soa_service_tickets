using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class CustomerMapper
    {
        public static Customer ToModel(CustomerRegisterRequest dto)
        {
            return new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
            };
        }

        public static CustomerAuthResponse ToAuthResponse(Customer model, string token, DateTime expiration)
        {
            return new CustomerAuthResponse
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Token = token,
                TokenExpiration = expiration
            };
        }

        public static CustomerResponse ToCustomerResponse(Customer model, string token, DateTime expiration)
        {
            return new CustomerResponse
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                // Token = token,
                //TokenExpiration = expiration
            };
        }

    }
}