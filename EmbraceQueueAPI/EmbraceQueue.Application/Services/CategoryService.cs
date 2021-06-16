using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Domain.Dtos.Categories;

namespace EmbraceQueue.Application.Services
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

        public async Task<IEnumerable<GetCategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync().ConfigureAwait(false);
            return categories.Select(x => Map(x));
        }

        public async Task<GetCategoryDto> FindCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.FindCategoryByIdAsync(id).ConfigureAwait(false);
            if (category != null) return Map(category);
            return null;
        }

        public async Task<IEnumerable<GetCategoryDto>> FindCategoriesByIdAsync(int id)
        {
            var categories = await _categoryRepository.FindCategoriesByIdAsync(id).ConfigureAwait(false);
            return categories.Select(x => Map(x));
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto category)
        {
            await _categoryRepository.UpdateCategoryAsync(Map(id, category)).ConfigureAwait(false);
        }

        public async Task<GetCategoryDto> AddCategoryAsync(CreateCategoryDto category)
        {
            var createdCategory = await _categoryRepository.AddCategoryAsync(Map(category)).ConfigureAwait(false);
            return Map(createdCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id).ConfigureAwait(false);
        }

        private static GetCategoryDto Map(Category category) => new GetCategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName
        };

        private static Category Map(int id, UpdateCategoryDto updateCategoryDto) => new Category
        {
            Id = id,
            CategoryName = updateCategoryDto.CategoryName
        };

        private static Category Map(CreateCategoryDto createCategoryDto) => new Category
        {
            CategoryName = createCategoryDto.CategoryName
        };
    }
}
