using PokemonReviewApp.Data;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IPokemonRepository Pokemons { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Country> Countries { get; private set; }
        public IGenericRepository<Owner> Owners { get; private set; }
        public IGenericRepository<Review> Reviews { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Pokemons = new PokemonRepository(_context);
            Categories = new GenericRepository<Category>(_context);
            Countries = new GenericRepository<Country>(_context);
            Owners = new GenericRepository<Owner>(_context);
            Reviews = new GenericRepository<Review>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}