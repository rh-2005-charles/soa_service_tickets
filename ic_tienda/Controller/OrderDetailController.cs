using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OrderDetailResponse>>> GetAll([FromQuery] QueryObject query)
        {
            try
            {
                var result = await _orderDetailService.GetAllAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailResponse>> GetById(int id)
        {
            try
            {
                var orderDetail = await _orderDetailService.GetByIdAsync(id);
                return Ok(orderDetail);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<List<OrderDetailResponse>>> GetByOrderId(int orderId)
        {
            try
            {
                var orderDetails = await _orderDetailService.GetByOrderIdAsync(orderId);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailResponse>> Create([FromBody] OrderDetailRequest request)
        {
            try
            {
                var createdOrderDetail = await _orderDetailService.AddAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = createdOrderDetail.Id }, createdOrderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetailResponse>> Update(int id, [FromBody] OrderDetailRequest request)
        {
            try
            {
                var updatedOrderDetail = await _orderDetailService.UpdateAsync(id, request);
                return Ok(updatedOrderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderDetailService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}