using EmbraceQueue.Infrastructure.Repositories;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmbraceQueue.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IWorkingDayRepository, WorkingDayRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceLineRepository, ServiceLineRepository>();
            services.AddScoped<IDigitalTicketRepository, DigitalTicketRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IServicesServiceLineRepository, ServicesServiceLineRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
