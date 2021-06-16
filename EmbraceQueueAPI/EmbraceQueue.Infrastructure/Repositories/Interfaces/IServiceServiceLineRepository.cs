using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IServicesServiceLineRepository
    {
        Task<ServicesServiceLine> FindServicesServiceLine(int serviceId, int serviceLineId);
        Task<IEnumerable<ServicesServiceLine>> GetServicesServiceLinesAsync();
        Task<IEnumerable<ServicesServiceLine>> FindAllServices(int serviceId);
        Task<IEnumerable<ServicesServiceLine>> FindAllServiceLines(int serviceLineId);
        Task<ServicesServiceLine> AddServiceServiceLineAsync(ServicesServiceLine serviceServiceLine);
        Task DeleteServiceServiceLineAsync(int serviceId, int serviceLineId);
    }
}
