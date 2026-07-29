using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonService(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        public ICollection<PokemonDto> GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            return _mapper.Map<List<PokemonDto>>(pokemons);
        }

        public PokemonDto? GetPokemon(int id)
        {
            if (!_pokemonRepository.PokemonExists(id)) return null;
            return _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));
        }

        public PokemonDto? GetPokemon(string name)
        {
            var pokemon = _pokemonRepository.GetPokemon(name);
            return _mapper.Map<PokemonDto>(pokemon);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return 0;
            return _pokemonRepository.GetPokemonRating(pokeId);
        }

        public bool PokemonExists(int pokeId) => _pokemonRepository.PokemonExists(pokeId);

        public bool CreatePokemon(int ownerId, int categoryId, PokemonDto pokemonCreate)
        {
            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);
            return _pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap);
        }

        public bool UpdatePokemon(int ownerId, int categoryId, PokemonDto pokemonUpdate)
        {
            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);
            return _pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap);
        }

        public bool DeletePokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return false;
            var pokemonToDelete = _pokemonRepository.GetPokemon(pokeId);
            return _pokemonRepository.DeletePokemon(pokemonToDelete);
        }
    }
}