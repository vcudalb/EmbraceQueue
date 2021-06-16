using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Services;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<GetServiceDto>> GetServicesAsync()
        {
            var services = await _serviceRepository.GetServicesAsync().ConfigureAwait(false);
            return services.Select(x => Map(x));
        }

        public async Task<GetServiceDto> FindServiceByIdAsync(int id)
        {
            var service = await _serviceRepository.FindServiceByIdAsync(id).ConfigureAwait(false);
            if (service != null) return Map(service);
            return null;
        }

        public async Task<IEnumerable<GetServiceDto>> FindServicesByIdAsync(int id)
        {
            var services = await _serviceRepository.FindServicesByIdAsync(id).ConfigureAwait(false);
            return services.Select(x => Map(x));
        }

        public async Task UpdateServiceAsync(int id, UpdateServiceDto service)
        {
            await _serviceRepository.UpdateServiceAsync(Map(id, service)).ConfigureAwait(false);
        }

        public async Task<GetServiceDto> AddServiceAsync(CreateServiceDto service)
        {
            var createdService = await _serviceRepository.AddServiceAsync(Map(service)).ConfigureAwait(false);
            return Map(createdService);
        }

        public async Task DeleteServiceAsync(int id)
        {
            await _serviceRepository.DeleteServiceAsync(id).ConfigureAwait(false);
        }

        private static GetServiceDto Map(Service service) => new GetServiceDto
        {
            Id = service.Id,
            CompanyId = service.CompanyId,
            ServiceType = service.ServiceType,
            RecentIncrementedLineId = service.RecentIncrementedLineId
        };

        private static Service Map(int id, UpdateServiceDto updateServiceDto) => new Service
        {
            Id = id,
            CompanyId = updateServiceDto.CompanyId,
            ServiceType = updateServiceDto.ServiceType,
            RecentIncrementedLineId = updateServiceDto.RecentIncrementedLineId
        };

        private static Service Map(CreateServiceDto createServiceDto) => new Service
        {
            CompanyId = createServiceDto.CompanyId,
            ServiceType = createServiceDto.ServiceType,
            RecentIncrementedLineId = createServiceDto.RecentIncrementedLineId
        };
    }
}
