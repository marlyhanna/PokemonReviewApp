using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public IActionResult GetOwners() => Ok(_ownerService.GetOwners());

        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerService.OwnerExists(ownerId)) return NotFound();
            return Ok(_ownerService.GetOwner(ownerId));
        }

        [HttpGet("{ownerId}/pokemon")]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerService.OwnerExists(ownerId)) return NotFound();
            return Ok(_ownerService.GetPokemonByOwner(ownerId));
        }

        [HttpPost]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null) return BadRequest(ModelState);

            if (!_ownerService.CreateOwner(countryId, ownerCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null || ownerId != ownerUpdate.Id) return BadRequest(ModelState);
            if (!_ownerService.OwnerExists(ownerId)) return NotFound();

            if (!_ownerService.UpdateOwner(ownerUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerService.OwnerExists(ownerId)) return NotFound();

            if (!_ownerService.DeleteOwner(ownerId))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}