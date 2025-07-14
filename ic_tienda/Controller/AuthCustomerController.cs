using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthCustomerController : ControllerBase
    {
        private readonly IAuthCustomerService _service;
        public AuthCustomerController(IAuthCustomerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _service.GetById(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<CustomerAuthResponse>> Login([FromBody] CustomerLoginRequest request)
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
        public async Task<ActionResult<CustomerAuthResponse>> Register([FromBody] CustomerRegisterRequest request)
        {
            try
            {
                var authResponse = await _service.Register(request);
                return CreatedAtAction(nameof(GetCustomerById), new { id = authResponse.Id }, authResponse);
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result = await _service.CancelOrderAsync(orderId);

            return Ok(result);
        }
    }
}