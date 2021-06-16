using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> FindCompanyByIdAsync(int id);
        Task<IEnumerable<Company>> FindCompaniesByIdAsync(int id);
        Task UpdateCompanyAsync(Company company);
        Task<Company> AddCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
    }
}
