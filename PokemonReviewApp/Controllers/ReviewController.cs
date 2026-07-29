using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetReviews() => Ok(_reviewService.GetReviews());

        [HttpGet("{reviewId}")]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewService.ReviewExists(reviewId)) return NotFound();
            return Ok(_reviewService.GetReview(reviewId));
        }

        [HttpGet("pokemon/{pokeId}")]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
            return Ok(_reviewService.GetReviewsOfAPokemon(pokeId));
        }

        [HttpPost]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null) return BadRequest(ModelState);

            if (!_reviewService.CreateReview(reviewerId, pokeId, reviewCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if (reviewUpdate == null || reviewId != reviewUpdate.Id) return BadRequest(ModelState);
            if (!_reviewService.ReviewExists(reviewId)) return NotFound();

            if (!_reviewService.UpdateReview(reviewUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewService.ReviewExists(reviewId)) return NotFound();

            if (!_reviewService.DeleteReview(reviewId))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}