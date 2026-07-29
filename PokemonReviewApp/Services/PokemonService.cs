using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ICollection<PokemonDto> GetPokemons()
        {
            var pokemons = _unitOfWork.Pokemons.GetAllAsync().GetAwaiter().GetResult();
            return _mapper.Map<ICollection<PokemonDto>>(pokemons);
        }

        public PokemonDto? GetPokemon(int id)
        {
            var pokemon = _unitOfWork.Pokemons.GetPokemon(id);
            return pokemon == null ? null : _mapper.Map<PokemonDto>(pokemon);
        }

        public PokemonDto? GetPokemon(string name)
        {
            var pokemon = _unitOfWork.Pokemons.GetPokemon(name);
            return pokemon == null ? null : _mapper.Map<PokemonDto>(pokemon);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            return _unitOfWork.Pokemons.GetPokemonRating(pokeId);
        }

        public bool PokemonExists(int pokeId)
        {
            return _unitOfWork.Pokemons.PokemonExists(pokeId);
        }

        public bool CreatePokemon(int ownerId, int categoryId, PokemonDto pokemonCreate)
        {
            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);
            if (!_unitOfWork.Pokemons.CreatePokemon(ownerId, categoryId, pokemonMap))
                return false;

            return _unitOfWork.CompleteAsync().GetAwaiter().GetResult() > 0;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, PokemonDto pokemonUpdate)
        {
            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);
            if (!_unitOfWork.Pokemons.UpdatePokemon(ownerId, categoryId, pokemonMap))
                return false;

            return _unitOfWork.CompleteAsync().GetAwaiter().GetResult() > 0;
        }

        public bool DeletePokemon(int pokeId)
        {
            var pokemon = _unitOfWork.Pokemons.GetPokemon(pokeId);
            if (pokemon == null)
                return false;

            _unitOfWork.Pokemons.Delete(pokemon);
            return _unitOfWork.CompleteAsync().GetAwaiter().GetResult() > 0;
        }
    }
}