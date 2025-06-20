using System.Security.Cryptography;
using System.Text;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IRepositories;
using ic_tienda_business.IServices;

namespace ic_tienda_data.Services
{
    public class AuthCustomerService : IAuthCustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtTokenService _tokenService;

        public AuthCustomerService(
            ICustomerRepository customerRepository,
            IJwtTokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        public async Task<CustomerAuthResponse> Login(CustomerLoginRequest request)
        {
            // Agrega logs para diagnóstico
            // Console.WriteLine($"Intento de login con email: {request.Email}");

            var customer = await _customerRepository.GetByEmail(request.Email);

            if (customer == null)
            {
                Console.WriteLine("Usuario no encontrado");
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            Console.WriteLine($"Usuario encontrado: {customer.Email}");

            // Verificación con BCrypt
            if (!BCrypt.Net.BCrypt.Verify(request.Password, customer.Password))
            {
                Console.WriteLine("Contraseña no coincide");
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            var token = _tokenService.GenerateToken(customer);

            return new CustomerAuthResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<CustomerAuthResponse> Register(CustomerRegisterRequest request)
        {
            var existingCustomer = await _customerRepository.GetByEmail(request.Email);
            if (existingCustomer != null)
                throw new ArgumentException("El email ya está registrado");

            var customerResponse = await _customerRepository.Create(request);

            // Obtener el modelo completo para generar el token
            var customer = new CustomerResponse
            {
                Id = customerResponse.Id,
                Email = customerResponse.Email,
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName
            };

            var token = _tokenService.GenerateToken(customer);

            return new CustomerAuthResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddHours(1)
            };
        }


    }
}
