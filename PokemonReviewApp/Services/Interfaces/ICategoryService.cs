using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface ICategoryService
    {
        ICollection<CategoryDto> GetCategories();
        CategoryDto? GetCategory(int id);
        ICollection<PokemonDto> GetPokemonByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(CategoryDto categoryCreate);
        bool UpdateCategory(CategoryDto categoryUpdate);
        bool DeleteCategory(int categoryId);
    }
}