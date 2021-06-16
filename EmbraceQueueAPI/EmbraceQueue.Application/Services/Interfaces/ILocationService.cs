using EmbraceQueue.Domain.Dtos.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<GetLocationDto>> GetLocationsAsync();
        Task<GetLocationDto> FindLocationByIdAsync(int id);
        Task<IEnumerable<GetLocationDto>> FindLocationsByIdAsync(int id);
        Task UpdateLocationAsync(int id, UpdateLocationDto location);
        Task<GetLocationDto> AddLocationAsync(CreateLocationDto location);
        Task DeleteLocationAsync(int id);
    }
}
