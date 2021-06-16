using EmbraceQueue.Domain.Dtos.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<GetServiceDto>> GetServicesAsync();
        Task<GetServiceDto> FindServiceByIdAsync(int id);
        Task<IEnumerable<GetServiceDto>> FindServicesByIdAsync(int id);
        Task UpdateServiceAsync(int id, UpdateServiceDto service);
        Task<GetServiceDto> AddServiceAsync(CreateServiceDto service);
        Task DeleteServiceAsync(int id);
    }
}
