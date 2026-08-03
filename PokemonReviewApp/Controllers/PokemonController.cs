using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        // GET: api/Pokemon (Public)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemons()
        {
            var result = await _pokemonService.GetPokemonsAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // GET: api/Pokemon/5 (Public)
        [HttpGet("{pokeId:int}")]
        [ProducesResponseType(200, Type = typeof(PokemonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPokemon(int pokeId)
        {
            var result = await _pokemonService.GetPokemonByIdAsync(pokeId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        // GET: api/Pokemon/5/rating (Public)
        [HttpGet("{pokeId:int}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPokemonRating(int pokeId)
        {
            var result = await _pokemonService.GetPokemonRatingAsync(pokeId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        // POST: api/Pokemon (Authenticated Users)
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest("Pokemon payload cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pokemonService.CreatePokemonAsync(ownerId, categoryId, pokemonCreate);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok("Successfully created.");
        }

        // PUT: api/Pokemon/5 (Authenticated Users)
        [HttpPut("{pokeId:int}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePokemon(int pokeId, [FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto updatedPokemon)
        {
            if (updatedPokemon == null)
                return BadRequest("Pokemon payload cannot be null.");

            if (pokeId != updatedPokemon.Id)
                return BadRequest("ID mismatch between route and request body.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pokemonService.UpdatePokemonAsync(ownerId, categoryId, updatedPokemon);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        // DELETE: api/Pokemon/5 (Admin Users Only)
        [HttpDelete("{pokeId:int}")]
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePokemon(int pokeId)
        {
            var result = await _pokemonService.DeletePokemonAsync(pokeId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok("Successfully deleted.");
        }
    }
}