using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : GenericRepository<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(DataContext context) : base(context)
        {
        }

        public Pokemon? GetPokemonByName(string name)
        {
            return _dbSet.FirstOrDefault(p => p.Name == name);
        }

        public Pokemon? GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            var trimmedName = pokemonCreate.Name.Trim().ToUpper();
            return _dbSet.FirstOrDefault(c => c.Name.Trim().ToUpper() == trimmedName);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (!review.Any())
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool PokemonExists(int pokeId)
        {
            return _dbSet.Any(p => p.Id == pokeId);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.FirstOrDefault(a => a.Id == ownerId);
            var category = _context.Categories.FirstOrDefault(a => a.Id == categoryId);

            if (pokemonOwnerEntity == null || category == null)
                return false;

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
            _context.Add(pokemonCategory);

            _dbSet.Add(pokemon);

            return true;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _dbSet.Update(pokemon);
            return true;
        }
    }
}