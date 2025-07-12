using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EventRequest request)
        {

            var createdCategory = await _service.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
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
        public async Task<IActionResult> UpdateById(int id, [FromForm] EventRequest request)
        {
            await _service.UpdateAsync(id, request);

            return Ok(new { message = "Item Actualizado." });
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelEvent(int id)
        {
            await _service.CancelEventAsync(id);

            return Ok(new { message = "Item cancelado." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Item eliminado." });
        }


    }
}