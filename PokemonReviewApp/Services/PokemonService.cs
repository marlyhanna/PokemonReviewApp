// Services/PokemonService.cs
using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Exceptions;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Services
{
    public class PokemonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PokemonDto>> GetAllPokemonsAsync()
        {
            var pokemons = await _unitOfWork.Pokemons.GetAllAsync();
            return _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
        }

        public async Task CreatePokemonAsync(PokemonDto pokemonDto)
        {
            var pokemon = _mapper.Map<Pokemon>(pokemonDto);

            // Add entity through unit of work
            await _unitOfWork.Pokemons.AddAsync(pokemon);

            // Save transaction cleanly
            await _unitOfWork.CompleteAsync();
        }
    }
}