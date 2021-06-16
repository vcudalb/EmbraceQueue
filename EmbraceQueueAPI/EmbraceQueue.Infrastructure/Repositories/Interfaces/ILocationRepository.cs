using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocationsAsync();
        Task<Location> FindLocationByIdAsync(int id);
        Task<IEnumerable<Location>> FindLocationsByIdAsync(int id);
        Task UpdateLocationAsync(Location location);
        Task<Location> AddLocationAsync(Location location);
        Task DeleteLocationAsync(int id);
    }
}
