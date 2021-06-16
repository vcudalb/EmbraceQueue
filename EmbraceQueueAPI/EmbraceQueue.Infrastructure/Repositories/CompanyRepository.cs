using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public CompanyRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _dbContext.Companies.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Company> FindCompanyByIdAsync(int id)
        {
            return await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> FindCompaniesByIdAsync(int id)
        {
            return await _dbContext.Companies.AsNoTracking().Where(c => c.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == company.Id).ConfigureAwait(false);

            if (existingCompany.CategoryId != company.CategoryId && company.CategoryId > 0) existingCompany.CategoryId = company.CategoryId;
            if (!string.Equals(existingCompany.CompanyName, company.CompanyName, StringComparison.InvariantCultureIgnoreCase)) existingCompany.CompanyName = company.CompanyName;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            var isAnExistingCompany = await _dbContext.Companies.AnyAsync(c => !string.IsNullOrEmpty(c.CompanyName)
            && c.CompanyName.ToLower() == company.CompanyName.ToLower()
            && c.CategoryId == company.CategoryId).ConfigureAwait(false);

            if (isAnExistingCompany) throw new Exception($"Company with CompanyName: {company.CompanyName} and CategoryId: {company.CategoryId} already exists.");

            await _dbContext.AddAsync(company).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return company;
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            _dbContext.Companies.Remove(existingCompany);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
