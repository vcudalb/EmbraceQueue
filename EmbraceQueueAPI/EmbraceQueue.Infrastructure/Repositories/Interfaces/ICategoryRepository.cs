using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> FindCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> FindCategoriesByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
