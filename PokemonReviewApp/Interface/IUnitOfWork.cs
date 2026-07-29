using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface IUnitOfWork : IDisposable
    {
     
        IPokemonRepository Pokemons { get; }

        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Owner> Owners { get; }
        IGenericRepository<Review> Reviews { get; }

        // Commit all changes across repositories in a single transaction
        Task<int> CompleteAsync();
    }
}