using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IServiceLineRepository
    {
        Task<IEnumerable<ServiceLine>> GetServiceLinesAsync();
        Task<ServiceLine> FindServiceLineByIdAsync(int id);
        Task<IEnumerable<ServiceLine>> FindServiceLinesByIdAsync(int id);
        Task UpdateServiceLineAsync(ServiceLine location);
        Task<ServiceLine> AddServiceLineAsync(ServiceLine location);
        Task DeleteServiceLineAsync(int id);
    }
}
