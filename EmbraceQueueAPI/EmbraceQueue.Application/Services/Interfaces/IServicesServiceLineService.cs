using EmbraceQueue.Domain.Dtos.ServicesServiceLines;
using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IServicesServiceLineService
    {
        Task<ServicesServiceLineDto> FindServicesServiceLine(int serviceId, int serviceLineId);
        Task<IEnumerable<ServicesServiceLineDto>> GetServicesServiceLinesAsync();
        Task<IEnumerable<ServicesServiceLineDto>> FindAllServices(int serviceId);
        Task<IEnumerable<ServicesServiceLineDto>> FindAllServiceLines(int serviceLineId);
        Task<ServicesServiceLineDto> AddServiceServiceLineAsync(ServicesServiceLineDto serviceServiceLineDto);
        Task DeleteServiceServiceLineAsync(int serviceId, int serviceLineId);
    }
}
