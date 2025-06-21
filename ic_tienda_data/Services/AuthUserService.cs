using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenService _jwtToken;

        public AuthUserService(IUserRepository repository, IJwtTokenService jwtToken)
        {
            _jwtToken = jwtToken;
            _repository = repository;
        }

        public async Task<UserResponse> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<UserAuthResponse> Login(UserLoginRequest request)
        {
            var user = await _repository.GetByEmail(request.Email);

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado");
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            Console.WriteLine($"Usuario encontrado: {user.Email}");

            // Verificación con BCrypt
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                Console.WriteLine("Contraseña no coincide");
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            var token = _jwtToken.GenerateUserToken(user);

            return new UserAuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<UserAuthResponse> Register(UserRegisterRequest request)
        {
            // Verificar si el email ya está registrado
            var existingUser = await _repository.GetByEmail(request.Email);
            if (existingUser != null)
                throw new Exception("El email ya está registrado");

            // Crear nuevo usuario
            var response = await _repository.Create(request);

            // Obtener el modelo completo para generar el token
            var user = new UserResponse
            {
                Id = response.Id,
                Email = response.Email,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Role = response.Role
            };

            // Generar token JWT para el nuevo usuario
            var token = _jwtToken.GenerateUserToken(user);

            return new UserAuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}