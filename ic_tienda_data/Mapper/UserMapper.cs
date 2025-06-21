using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_data.sources.BaseDeDatos.Models;

namespace ic_tienda_data.Mapper
{
    public static class UserMapper
    {
        public static User ToModel(UserRegisterRequest request)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Phone = request.Phone,
                Role = request.Role
            };
        }

        public static UserAuthResponse ToAuthResponse(User model, string token, DateTime expiration)
        {
            return new UserAuthResponse
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                Token = token,
                TokenExpiration = expiration
            };
        }

        public static UserResponse ToUserResponse(User model, string token, DateTime expiration)
        {
            return new UserResponse
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Role = model.Role
            };
        }

        public static UserResponse ToResponse(User model)
        {
            return new UserResponse
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Role = model.Role
            };
        }
    }
}