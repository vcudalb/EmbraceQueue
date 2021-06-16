using EmbraceQueue.Domain.Dtos.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetCategoriesAsync();
        Task<GetCategoryDto> FindCategoryByIdAsync(int id);
        Task<IEnumerable<GetCategoryDto>> FindCategoriesByIdAsync(int id);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto category);
        Task<GetCategoryDto> AddCategoryAsync(CreateCategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}
