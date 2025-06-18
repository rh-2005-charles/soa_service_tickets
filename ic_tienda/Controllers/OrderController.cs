/* using ic_tienda.Attributes;
using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderRequest request)
        {
            var result = await _orderService.AddAsync(request);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
        }

        [HttpGet]
        [IsLogin]
        [HasRoles("admin", "pedidoVer")]
        public async Task<IActionResult> GetAllOrders([FromQuery] QueryObjectOrder query)
        {
            var result = await _orderService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("in-process")]
        [IsLogin]
        [HasRoles("admin", "pedidoVer")]
        public async Task<IActionResult> GetAllInProcessOrders([FromQuery] QueryObjectOrder query)
        {
            var result = await _orderService.GetAllInProcessAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [IsLogin]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/details")]
        [IsLogin]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var result = await _orderService.GetOrderDetailsAsync(id);
            return Ok(result);
        }

        //[HttpGet("status")]
        [HttpGet("customer/{id}")]
        [IsLogin]
        public async Task<IActionResult> GetOrdersByStatus(int id, [FromQuery] QueryObjectOrderTwo query)
        {
            query.CustomerId = id;
            var result = await _orderService.GetOrdersByStatusAsync(query);
            return Ok(result);
        }

        [HttpGet("customer/{customerId}/in-process")]
        [IsLogin]
        public async Task<IActionResult> GetInProcessOrdersByCustomerId(int customerId)
        {
            var result = await _orderService.GetInProcessOrdersByCustomerIdAsync(customerId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [IsLogin]
        [HasRoles("admin", "pedidoEditar")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderRequest request)
        {
            var result = await _orderService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [IsLogin]
        [HasRoles("admin")]
        public async Task<IActionResult> DeleteOrder([FromBody] OrderResponse request)
        {
            await _orderService.DeleteAsync(request);
            return NoContent();
        }
    }
} */