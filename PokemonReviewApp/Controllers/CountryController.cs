using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetCountries() => Ok(_countryService.GetCountries());

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryService.CountryExists(countryId)) return NotFound();
            return Ok(_countryService.GetCountry(countryId));
        }

        [HttpGet("owners/{ownerId}")]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            return Ok(_countryService.GetCountryByOwner(ownerId));
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null) return BadRequest(ModelState);

            if (!_countryService.CreateCountry(countryCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{countryId}")]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto countryUpdate)
        {
            if (countryUpdate == null || countryId != countryUpdate.Id) return BadRequest(ModelState);
            if (!_countryService.CountryExists(countryId)) return NotFound();

            if (!_countryService.UpdateCountry(countryUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryService.CountryExists(countryId)) return NotFound();

            if (!_countryService.DeleteCountry(countryId))
            {
                ModelState.AddModelError("", "Something went wrong deleting country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}