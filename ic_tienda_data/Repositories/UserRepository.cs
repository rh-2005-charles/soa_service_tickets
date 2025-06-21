using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IRepositories;
using ic_tienda_data.Mapper;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IcTiendaDbContext _context;
        public UserRepository(IcTiendaDbContext context)
        {
            _context = context;
        }

        public async Task<UserAuthResponse> Create(UserRegisterRequest user)
        {
            var userMap = UserMapper.ToModel(user);
            _context.Users.Add(userMap);
            await _context.SaveChangesAsync();

            return UserMapper.ToAuthResponse(userMap, "", DateTime.MinValue);
        }

        public async Task<UserResponse?> GetByEmail(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    Phone = u.Phone,
                    Role = u.Role
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserResponse> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return UserMapper.ToResponse(user);
        }



    }
}