using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.Helpers;
using ic_tienda_business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return bad request if model state is invalid
            }

            var createdCategory = await _categoryService.AddAsync(categoryRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var categories = await _categoryService.GetAllAsync(query);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

    

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromForm] CategoryRequest categoryRequest)
        {
            await _categoryService.UpdateAsync(id, categoryRequest);

            return Ok(new { message = "Categoría actualizada exitosamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            //return NoContent();
            return Ok(new { message = "Categoría actualizada exitosamente." });
        }
    }
}