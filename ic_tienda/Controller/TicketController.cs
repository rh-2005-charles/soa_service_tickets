using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Dtos.Responses;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<TicketResponse>>> GetAll([FromQuery] QueryObject query)
        {
            try
            {
                var tickets = await _ticketService.GetAllAsync(query);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TicketResponse>>> GetById(int id)
        {
            try
            {
                var tickets = await _ticketService.GetByIdAsync(id);
                return Ok(tickets);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("order-detail/{orderDetailId}")]
        public async Task<ActionResult<List<TicketResponse>>> GetByOrderDetailId(int orderDetailId)
        {
            try
            {
                var tickets = await _ticketService.GetByOrderDetailIdAsync(orderDetailId);
                return Ok(tickets);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketRequest request)
        {
            try
            {
                var createdTicket = await _ticketService.AddAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = createdTicket.Id }, createdTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TicketResponse>> Update(int id, [FromBody] TicketRequest request)
        {
            try
            {
                var updatedTicket = await _ticketService.UpdateAsync(id, request);
                return Ok(updatedTicket);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
                await _ticketService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}