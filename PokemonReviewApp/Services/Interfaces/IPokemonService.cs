using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<Result<ICollection<PokemonDto>>> GetPokemonsAsync();
        Task<Result<PokemonDto>> GetPokemonByIdAsync(int id);
        Task<Result<PokemonDto>> GetPokemonByNameAsync(string name);
        Task<Result<decimal>> GetPokemonRatingAsync(int pokeId);
        Task<bool> PokemonExistsAsync(int pokeId);
        Task<Result> CreatePokemonAsync(int ownerId, int categoryId, PokemonDto pokemonCreate);
        Task<Result> UpdatePokemonAsync(int ownerId, int categoryId, PokemonDto pokemonUpdate);
        Task<Result> DeletePokemonAsync(int pokeId);
    }
}