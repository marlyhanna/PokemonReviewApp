using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ICollection<CategoryDto> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public CategoryDto? GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id)) return null;
            return _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
        }

        public ICollection<PokemonDto> GetPokemonByCategory(int categoryId)
        {
            var pokemons = _categoryRepository.GetPokemonByCategory(categoryId);
            return _mapper.Map<List<PokemonDto>>(pokemons);
        }

        public bool CategoryExists(int id) => _categoryRepository.CategoryExists(id);

        public bool CreateCategory(CategoryDto categoryCreate)
        {
            var categoryMap = _mapper.Map<Category>(categoryCreate);
            return _categoryRepository.CreateCategory(categoryMap);
        }

        public bool UpdateCategory(CategoryDto categoryUpdate)
        {
            var categoryMap = _mapper.Map<Category>(categoryUpdate);
            return _categoryRepository.UpdateCategory(categoryMap);
        }

        public bool DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId)) return false;
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            return _categoryRepository.DeleteCategory(categoryToDelete);
        }
    }
}