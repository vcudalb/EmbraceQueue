using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.ServicesServiceLines;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class ServicesServiceLineService : IServicesServiceLineService
    {
        private readonly IServicesServiceLineRepository _serviceRepository;

        public ServicesServiceLineService(IServicesServiceLineRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServicesServiceLineDto> FindServicesServiceLine(int serviceId, int serviceLineId)
        {
            var servicesServiceLine = await _serviceRepository.FindServicesServiceLine(serviceId, serviceLineId).ConfigureAwait(false);
            return Map(servicesServiceLine);
        }
        public async Task<IEnumerable<ServicesServiceLineDto>> GetServicesServiceLinesAsync()
        {
            var services = await _serviceRepository.GetServicesServiceLinesAsync().ConfigureAwait(false);
            return services.Select(x => Map(x));
        }
        public async Task<IEnumerable<ServicesServiceLineDto>> FindAllServices(int serviceId)
        {
            var services = await _serviceRepository.FindAllServices(serviceId).ConfigureAwait(false);
            return services.Select(x => Map(x));
        }
        public async Task<IEnumerable<ServicesServiceLineDto>> FindAllServiceLines(int serviceLineId)
        {
            var services = await _serviceRepository.FindAllServiceLines(serviceLineId).ConfigureAwait(false);
            return services.Select(x => Map(x));
        }
        public async Task<ServicesServiceLineDto> AddServiceServiceLineAsync(ServicesServiceLineDto serviceServiceLineDto)
        {
            var createdServicesServiceLine = await _serviceRepository.AddServiceServiceLineAsync(Map(serviceServiceLineDto)).ConfigureAwait(false);
            return Map(createdServicesServiceLine);
        }
        public async Task DeleteServiceServiceLineAsync(int serviceId, int serviceLineId)
        {
            await _serviceRepository.DeleteServiceServiceLineAsync(serviceId, serviceLineId);
        }

        private static ServicesServiceLineDto Map(ServicesServiceLine service) => new ServicesServiceLineDto
        {
            ServiceId = service.ServiceId,
            ServiceLineId = service.ServiceLineId
        };

        private static ServicesServiceLine Map(ServicesServiceLineDto service) => new ServicesServiceLine
        {
            ServiceId = service.ServiceId,
            ServiceLineId = service.ServiceLineId
        };
    }
}
