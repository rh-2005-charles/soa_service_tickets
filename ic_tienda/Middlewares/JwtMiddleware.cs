/* using System.Net;
using ic_tienda_business.Admin.Jwt;
using ic_tienda_business.IServices.Auth;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(
        RequestDelegate next,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            var token = GetTokenFromHeaders(context.Request.Headers);

            if (!string.IsNullOrEmpty(token))
            {
                await ValidateTokenAndSetContext(context, token);
            }

            await _next(context);
        }
        catch (SecurityTokenExpiredException ex)
        {
            _logger.LogWarning("Token expirado: {Message}", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                TitleMessage = "Token expirado",
                TextMessage = "Su sesión ha expirado, por favor inicie sesión nuevamente"
            });
        }
        catch (SecurityTokenValidationException ex)
        {
            _logger.LogWarning("Token inválido: {Message}", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                TitleMessage = "Token inválido",
                TextMessage = "El token proporcionado no es válido"
            });
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error en el middleware JWT");
            //throw; // Esto será capturado por el ErrorMiddleware
            _logger.LogError(ex, "Error en el middleware JWT");
            // NO relanzar la excepción, dejar que el ErrorMiddleware la capture
            throw;
        }
    }

    private string GetTokenFromHeaders(IHeaderDictionary headers)
    {
        if (headers.TryGetValue("Authorization", out StringValues authHeader))
        {
            return authHeader.FirstOrDefault()?.Split(" ").Last();
        }
        return null;
    }

    private async Task ValidateTokenAndSetContext(HttpContext context, string token)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var userTokenGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGeneratorUser>();
        var customerTokenGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();

        try
        {

            var userProfile = userTokenGenerator.ValidateToken(token);
            if (userProfile != null)
            {
                context.Items["profile"] = userProfile;
                _logger.LogInformation("Perfil de usuario asignado: {Email}, Roles: {Roles}",
                    userProfile.Email,
                    string.Join(", ", userProfile.Permissions.Select(r => r.Name)));
                return;
            }

            // Si llegamos aquí, el token no es válido para ningún perfil
            var customerProfile = customerTokenGenerator.ValidateToken(token);
            if (customerProfile != null)
            {
                context.Items["customerProfile"] = customerProfile;
                _logger.LogInformation("Perfil de cliente asignado: {Email}, ID: {Id}",
                    customerProfile.Email,
                    customerProfile.Id);
                return;
            }
            _logger.LogWarning("Token no válido para usuario ni cliente");
            throw new SecurityTokenValidationException("Token no válido");
        }
        catch (SecurityTokenExpiredException)
        {
            throw; // Relanzar para que se maneje en el Invoke
        }
        catch (SecurityTokenValidationException)
        {
            throw; // Relanzar para que se maneje en el Invoke
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar el token");
            throw; // Relanzar para que el ErrorMiddleware lo capture
        }

        //_logger.LogWarning("Token no válido para usuario ni cliente");
    }
} */