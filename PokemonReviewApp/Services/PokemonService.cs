using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
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

        public async Task<Result<ICollection<PokemonDto>>> GetPokemonsAsync()
        {
            var pokemons = await _unitOfWork.Repository<Pokemon>().GetAllAsync();
            var pokemonDtos = _mapper.Map<ICollection<PokemonDto>>(pokemons);

            return Result.Success(pokemonDtos);
        }

        public async Task<Result<PokemonDto>> GetPokemonByIdAsync(int id)
        {
            var pokemon = await _unitOfWork.Repository<Pokemon>().GetByIdAsync(id);
            if (pokemon == null)
                return Result.Failure<PokemonDto>($"Pokemon with ID {id} was not found.");

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);
            return Result.Success(pokemonDto);
        }

        public async Task<Result<PokemonDto>> GetPokemonByNameAsync(string name)
        {
            var pokemons = await _unitOfWork.Repository<Pokemon>().GetAllAsync();
            var pokemon = pokemons.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (pokemon == null)
                return Result.Failure<PokemonDto>($"Pokemon with name '{name}' was not found.");

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);
            return Result.Success(pokemonDto);
        }

        public async Task<Result<decimal>> GetPokemonRatingAsync(int pokeId)
        {
            var pokemon = await _unitOfWork.Repository<Pokemon>().GetByIdAsync(pokeId);
            if (pokemon == null)
                return Result.Failure<decimal>($"Pokemon with ID {pokeId} was not found.");

            // Assuming Reviews relationship exists on Pokemon model
            if (pokemon.Reviews == null || !pokemon.Reviews.Any())
                return Result.Success(0m);

            var rating = (decimal)pokemon.Reviews.Average(r => r.Rating);
            return Result.Success(rating);
        }

        public async Task<bool> PokemonExistsAsync(int pokeId)
        {
            var pokemon = await _unitOfWork.Repository<Pokemon>().GetByIdAsync(pokeId);
            return pokemon != null;
        }

        public async Task<Result> CreatePokemonAsync(int ownerId, int categoryId, PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return Result.Failure("Pokemon data cannot be null.");

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            await _unitOfWork.Repository<Pokemon>().AddAsync(pokemonMap);
            var saved = await _unitOfWork.CompleteAsync() > 0;

            return saved
                ? Result.Success()
                : Result.Failure("An error occurred while saving the Pokemon.");
        }

        public async Task<Result> UpdatePokemonAsync(int ownerId, int categoryId, PokemonDto pokemonUpdate)
        {
            if (pokemonUpdate == null)
                return Result.Failure("Pokemon data cannot be null.");

            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);

            _unitOfWork.Repository<Pokemon>().Update(pokemonMap);
            var saved = await _unitOfWork.CompleteAsync() > 0;

            return saved
                ? Result.Success()
                : Result.Failure("An error occurred while updating the Pokemon.");
        }

        public async Task<Result> DeletePokemonAsync(int pokeId)
        {
            var pokemon = await _unitOfWork.Repository<Pokemon>().GetByIdAsync(pokeId);
            if (pokemon == null)
                return Result.Failure($"Pokemon with ID {pokeId} was not found.");

            _unitOfWork.Repository<Pokemon>().Delete(pokemon);
            var saved = await _unitOfWork.CompleteAsync() > 0;

            return saved
                ? Result.Success()
                : Result.Failure("An error occurred while deleting the Pokemon.");
        }
    }
}