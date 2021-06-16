using EmbraceQueue.Domain.Dtos.ServiceLines;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IServiceLineService
    {
        Task<IEnumerable<GetServiceLineDto>> GetServiceLinesAsync();
        Task<GetServiceLineDto> FindServiceLineByIdAsync(int id);
        Task<IEnumerable<GetServiceLineDto>> FindServiceLinesByIdAsync(int id);
        Task UpdateServiceLineAsync(int id, UpdateServiceLineDto serviceLine);
        Task<GetServiceLineDto> AddServiceLineAsync(CreateServiceLineDto serviceLine);
        Task DeleteServiceLineAsync(int id);
    }
}
