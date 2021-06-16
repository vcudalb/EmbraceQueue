using EmbraceQueue.Application.Services;
using EmbraceQueue.Application.Services.Interfaces;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EmbraceQueue.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IWorkingDayService, WorkingDayService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceLineService, ServiceLineService>();
            services.AddScoped<IDigitalTicketService, DigitalTicketService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IServicesServiceLineService, ServicesServiceLineService>();

            return services;
        }
    }
}
