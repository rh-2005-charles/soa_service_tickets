using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class CustomerAuthServiceSOAP : ICustomerAuthServiceSOAP
    {
        private readonly IAuthCustomerService _authService;

        public CustomerAuthServiceSOAP(IAuthCustomerService authService)
        {
            _authService = authService;
        }

        public CustomerAuthResponse Login(CustomerLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                throw new FaultException("El objeto request o el email no pueden ser nulos");
            }

            Console.WriteLine($"Email recibido: {request.Email}");
            Console.WriteLine($"Password recibido: {request.Password}");

            try
            {
                return _authService.Login(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex}");
                throw new FaultException(ex.Message);
            }
        }

        public CustomerAuthResponse Register(CustomerRegisterRequest request)
        {
            try
            {
                return _authService.Register(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error en Register: {ex}");
                throw;
            }
        }
    }
}

