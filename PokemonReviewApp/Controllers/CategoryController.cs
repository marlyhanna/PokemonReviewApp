using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories() => Ok(_categoryService.GetCategories());

        [HttpGet("{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId)) return NotFound();
            return Ok(_categoryService.GetCategory(categoryId));
        }

        [HttpGet("pokemon/{categoryId}")]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            return Ok(_categoryService.GetPokemonByCategory(categoryId));
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null) return BadRequest(ModelState);

            if (!_categoryService.CreateCategory(categoryCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate == null || categoryId != categoryUpdate.Id) return BadRequest(ModelState);
            if (!_categoryService.CategoryExists(categoryId)) return NotFound();

            if (!_categoryService.UpdateCategory(categoryUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId)) return NotFound();

            if (!_categoryService.DeleteCategory(categoryId))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}