using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public ICollection<CountryDto> GetCountries()
        {
            return _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
        }

        public CountryDto? GetCountry(int id)
        {
            if (!_countryRepository.CountryExists(id)) return null;
            return _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
        }

        public CountryDto? GetCountryByOwner(int ownerId)
        {
            var country = _countryRepository.GetCountryByOwner(ownerId);
            return _mapper.Map<CountryDto>(country);
        }

        public ICollection<OwnerDto> GetOwnersFromACountry(int countryId)
        {
            var owners = _countryRepository.GetOwnersFromACountry(countryId);
            return _mapper.Map<List<OwnerDto>>(owners);
        }

        public bool CountryExists(int id) => _countryRepository.CountryExists(id);

        public bool CreateCountry(CountryDto countryCreate)
        {
            var countryMap = _mapper.Map<Country>(countryCreate);
            return _countryRepository.CreateCountry(countryMap);
        }

        public bool UpdateCountry(CountryDto countryUpdate)
        {
            var countryMap = _mapper.Map<Country>(countryUpdate);
            return _countryRepository.UpdateCountry(countryMap);
        }

        public bool DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId)) return false;
            var countryToDelete = _countryRepository.GetCountry(countryId);
            return _countryRepository.DeleteCountry(countryToDelete);
        }
    }
}