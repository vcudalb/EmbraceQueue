using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public CategoryRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Category> FindCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> FindCategoriesByIdAsync(int id)
        {
            return await _dbContext.Categories.AsNoTracking().Where(c => c.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id).ConfigureAwait(false);

            existingCategory.CategoryName = category.CategoryName;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var isAnExistingCategory = await _dbContext.Categories.AnyAsync(c => !string.IsNullOrEmpty(c.CategoryName) && c.CategoryName.ToLower() == category.CategoryName.ToLower()).ConfigureAwait(false);
            if (isAnExistingCategory) throw new Exception($"Category with name: {category.CategoryName} already exists.");

            await _dbContext.AddAsync(category).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            _dbContext.Categories.Remove(existingCategory);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
