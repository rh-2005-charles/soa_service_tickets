using CoreWCF;
using ic_tienda.Contracts;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IServices;

namespace ic_tienda.Services
{
    [ServiceBehavior(Namespace = "http://tempuri.org/")]
    public class UserAuthServiceSOAP : IUserAuthServiceSOAP
    {
        private readonly IAuthUserService _service;
        public UserAuthServiceSOAP(IAuthUserService service)
        {
            _service = service;
        }

        public UserResponse GetById(int id)
        {
            try
            {
                return _service.GetById(id).GetAwaiter().GetResult();
            }
            catch
            {
                throw new FaultException($"User con ID {id} no encontrado.");
            }
        }

        public UserAuthResponse Login(UserLoginRequest request)
        {
            try
            {
                return _service.Login(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex}");
                throw new FaultException(ex.Message);
            }
        }

        public UserAuthResponse Register(UserRegisterRequest request)
        {
            try
            {
                return _service.Register(request).GetAwaiter().GetResult();
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