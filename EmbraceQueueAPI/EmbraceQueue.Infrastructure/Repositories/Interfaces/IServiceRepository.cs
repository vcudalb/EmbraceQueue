using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<Service> FindServiceByIdAsync(int id);
        Task<IEnumerable<Service>> FindServicesByIdAsync(int id);
        Task UpdateServiceAsync(Service location);
        Task<Service> AddServiceAsync(Service location);
        Task DeleteServiceAsync(int id);
    }
}
