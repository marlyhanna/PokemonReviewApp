using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public ICollection<OwnerDto> GetOwners()
        {
            return _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());
        }

        public OwnerDto? GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId)) return null;
            return _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));
        }

        public ICollection<OwnerDto> GetOwnerOfAPokemon(int pokeId)
        {
            return _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerOfAPokemon(pokeId));
        }

        public ICollection<PokemonDto> GetPokemonByOwner(int ownerId)
        {
            return _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));
        }

        public bool OwnerExists(int ownerId) => _ownerRepository.OwnerExists(ownerId);

        public bool CreateOwner(int countryId, OwnerDto ownerCreate)
        {
            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = _countryRepository.GetCountry(countryId);
            return _ownerRepository.CreateOwner(ownerMap);
        }

        public bool UpdateOwner(OwnerDto ownerUpdate)
        {
            var ownerMap = _mapper.Map<Owner>(ownerUpdate);
            return _ownerRepository.UpdateOwner(ownerMap);
        }

        public bool DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId)) return false;
            var ownerToDelete = _ownerRepository.GetOwner(ownerId);
            return _ownerRepository.DeleteOwner(ownerToDelete);
        }
    }
}