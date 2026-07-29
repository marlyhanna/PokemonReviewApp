using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerService _reviewerService;

        public ReviewerController(IReviewerService reviewerService)
        {
            _reviewerService = reviewerService;
        }

        [HttpGet]
        public IActionResult GetReviewers() => Ok(_reviewerService.GetReviewers());

        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerService.ReviewerExists(reviewerId)) return NotFound();
            return Ok(_reviewerService.GetReviewer(reviewerId));
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!_reviewerService.ReviewerExists(reviewerId)) return NotFound();
            return Ok(_reviewerService.GetReviewsByReviewer(reviewerId));
        }

        [HttpPost]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null) return BadRequest(ModelState);

            if (!_reviewerService.CreateReviewer(reviewerCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewerId}")]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto reviewerUpdate)
        {
            if (reviewerUpdate == null || reviewerId != reviewerUpdate.Id) return BadRequest(ModelState);
            if (!_reviewerService.ReviewerExists(reviewerId)) return NotFound();

            if (!_reviewerService.UpdateReviewer(reviewerUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewerId}")]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerService.ReviewerExists(reviewerId)) return NotFound();

            if (!_reviewerService.DeleteReviewer(reviewerId))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}