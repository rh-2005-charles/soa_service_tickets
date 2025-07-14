using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService _service;
        public TicketTypeController(ITicketTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TicketTypeRequest request)
        {

            var createdCategory = await _service.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TicketTypeQueryObject query)
        {
            var item = await _service.GetAllAsync(query);
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromForm] TicketTypeRequest request)
        {
            await _service.UpdateAsync(id, request);

            return Ok(new { message = "Item Actualizado." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Item eliminado." });
        }

    }
}
