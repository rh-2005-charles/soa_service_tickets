using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IAuthUserService _service;

        public AuthUserController(IAuthUserService authService)
        {
            _service = authService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            try
            {
                var user = await _service.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthResponse>> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var authResponse = await _service.Login(request);
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthResponse>> Register([FromBody] UserRegisterRequest request)
        {
            try
            {
                var authResponse = await _service.Register(request);
                return CreatedAtAction(nameof(GetUserById), new { id = authResponse.Id }, authResponse);
            }
            catch (Exception ex) when (ex.Message == "El email ya está registrado")
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}