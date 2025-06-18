/* using ic_tienda_business.Admin.Dtos;
using ic_tienda_business.Dtos.Responses.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ic_tienda.Attributes
{
    // Atributo para verificar roles
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasRolesAttribute : Attribute, IAuthorizationFilter
    {
        private readonly List<string> roles;

        public HasRolesAttribute(params string[] roles)
        {
            this.roles = roles.ToList();
        }

        // Verificar la autorización
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userProfile = (UserAppProfile?)context.HttpContext.Items["profile"];
            var customerProfile = (CustomerResponse?)context.HttpContext.Items["customerProfile"];

            // Verifica si el usuario o cliente no están logeados
            if (userProfile == null && customerProfile == null)
            {
                context.Result = new JsonResult(new { code = "401", message = "No estás logeado" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // Si es un usuario, verifica los roles
            if (userProfile != null)
            {
                var userRoles = userProfile.Permissions.Select(r => r.Name).ToList();

                // Verifica si el usuario tiene los roles requeridos
                if (!roles.Any(role => userRoles.Contains(role, StringComparer.OrdinalIgnoreCase)))
                {
                    context.Result = new JsonResult(new { code = "403", message = "No tienes permisos" })
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
            }
            else if (customerProfile != null)
            {
                // Si el cliente está intentando acceder a un recurso restringido, puedes manejarlo aquí
                context.Result = new JsonResult(new { code = "403", message = "No tienes permisos" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
} */