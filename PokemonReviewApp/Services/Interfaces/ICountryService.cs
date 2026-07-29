using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface ICountryService
    {
        ICollection<CountryDto> GetCountries();
        CountryDto? GetCountry(int id);
        CountryDto? GetCountryByOwner(int ownerId);
        ICollection<OwnerDto> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(CountryDto countryCreate);
        bool UpdateCountry(CountryDto countryUpdate);
        bool DeleteCountry(int countryId);
    }
}