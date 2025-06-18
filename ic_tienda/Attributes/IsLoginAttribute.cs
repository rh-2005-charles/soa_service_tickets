/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ic_tienda_business.Admin.Dtos;
using ic_tienda_business.Dtos.Responses.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ic_tienda.Attributes
{
    // Atributo para verificar si está logeado
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IsLoginAttribute : Attribute, IAuthorizationFilter
    {
        // Verificar la autorización
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userProfile = (UserAppProfile?)context.HttpContext.Items["profile"];
            var customerProfile = (CustomerResponse?)context.HttpContext.Items["customerProfile"];

            // Verificar si no están logeados
            if (userProfile == null && customerProfile == null)
            {
                context.Result = new JsonResult(new { code = "401", message = "No estás logeado" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
} */