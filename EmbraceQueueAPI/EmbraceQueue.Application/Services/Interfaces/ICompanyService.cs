using EmbraceQueue.Domain.Dtos.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<GetCompanyDto>> GetCompaniesAsync();
        Task<GetCompanyDto> FindCompanyByIdAsync(int id);
        Task<IEnumerable<GetCompanyDto>> FindCompaniesByIdAsync(int id);
        Task UpdateCompanyAsync(int id, UpdateCompanyDto company);
        Task<GetCompanyDto> AddCompanyAsync(CreateCompanyDto company);
        Task DeleteCompanyAsync(int id);
    }
}
