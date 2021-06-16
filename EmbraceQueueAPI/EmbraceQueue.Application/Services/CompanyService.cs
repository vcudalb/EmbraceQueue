using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Companies;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<GetCompanyDto>> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetCompaniesAsync().ConfigureAwait(false);
            return companies.Select(x => Map(x));
        }

        public async Task<GetCompanyDto> FindCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.FindCompanyByIdAsync(id).ConfigureAwait(false);
            if (company != null) return Map(company);
            return null;
        }

        public async Task<IEnumerable<GetCompanyDto>> FindCompaniesByIdAsync(int id)
        {
            var companies = await _companyRepository.FindCompaniesByIdAsync(id).ConfigureAwait(false);
            return companies.Select(x => Map(x));
        }

        public async Task UpdateCompanyAsync(int id, UpdateCompanyDto company)
        {
            await _companyRepository.UpdateCompanyAsync(Map(id, company)).ConfigureAwait(false);
        }

        public async Task<GetCompanyDto> AddCompanyAsync(CreateCompanyDto company)
        {
            var createdCompany = await _companyRepository.AddCompanyAsync(Map(company)).ConfigureAwait(false);
            return Map(createdCompany);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _companyRepository.DeleteCompanyAsync(id).ConfigureAwait(false);
        }

        private static GetCompanyDto Map(Company company) => new GetCompanyDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            CategoryId = company.CategoryId
        };

        private static Company Map(int id, UpdateCompanyDto updateCompanyDto) => new Company
        {
            Id = id,
            CompanyName = updateCompanyDto.CompanyName,
            CategoryId = updateCompanyDto.CategoryId
        };

        private static Company Map(CreateCompanyDto createCompanyDto) => new Company
        {
            CompanyName = createCompanyDto.CompanyName,
            CategoryId = createCompanyDto.CategoryId
        };
    }
}
