using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces // Or your interface namespace
{
    public interface IPokemonService
    {
        ICollection<PokemonDto> GetPokemons();
        PokemonDto? GetPokemon(int id);
        PokemonDto? GetPokemon(string name);
        bool PokemonExists(int pokeId);

      
        decimal GetPokemonRating(int pokeId);

        bool CreatePokemon(int ownerId, int categoryId, PokemonDto pokemonCreate);

      
        bool UpdatePokemon(int ownerId, int categoryId, PokemonDto pokemonUpdate);

        // 3. ADD THIS MISSING METHOD:
        bool DeletePokemon(int pokeId);
    }
}