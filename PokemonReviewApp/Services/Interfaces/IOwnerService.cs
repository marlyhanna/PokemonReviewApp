using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface IOwnerService
    {
        ICollection<OwnerDto> GetOwners();
        OwnerDto? GetOwner(int ownerId);
        ICollection<OwnerDto> GetOwnerOfAPokemon(int pokeId);
        ICollection<PokemonDto> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        bool CreateOwner(int countryId, OwnerDto ownerCreate);
        bool UpdateOwner(OwnerDto ownerUpdate);
        bool DeleteOwner(int ownerId);
    }
}