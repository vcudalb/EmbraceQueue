using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Locations;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<GetLocationDto>> GetLocationsAsync()
        {
            var locations = await _locationRepository.GetLocationsAsync().ConfigureAwait(false);
            return locations.Select(x => Map(x));
        }

        public async Task<GetLocationDto> FindLocationByIdAsync(int id)
        {
            var location = await _locationRepository.FindLocationByIdAsync(id).ConfigureAwait(false);
            if (location != null) return Map(location);
            return null;
        }

        public async Task<IEnumerable<GetLocationDto>> FindLocationsByIdAsync(int id)
        {
            var locations = await _locationRepository.FindLocationsByIdAsync(id).ConfigureAwait(false);
            return locations.Select(x => Map(x));
        }

        public async Task UpdateLocationAsync(int id, UpdateLocationDto location)
        {
            await _locationRepository.UpdateLocationAsync(Map(id, location)).ConfigureAwait(false);
        }

        public async Task<GetLocationDto> AddLocationAsync(CreateLocationDto location)
        {
            var createdLocation = await _locationRepository.AddLocationAsync(Map(location)).ConfigureAwait(false);
            return Map(createdLocation);
        }

        public async Task DeleteLocationAsync(int id)
        {
            await _locationRepository.DeleteLocationAsync(id).ConfigureAwait(false);
        }

        private static GetLocationDto Map(Location location) => new GetLocationDto
        {
            Id = location.Id,
            BranchId = location.BranchId,
            Area = location.Area,
            Building = location.Building,
            City = location.City,
            Mall = location.Mall,
            NearbyLandmark = location.NearbyLandmark,
            Street = location.Street
        };

        private static Location Map(int id, UpdateLocationDto updateLocationDto) => new Location
        {
            Id = id,
            BranchId = updateLocationDto.BranchId,
            Area = updateLocationDto.Area,
            Building = updateLocationDto.Building,
            City = updateLocationDto.City,
            Mall = updateLocationDto.Mall,
            NearbyLandmark = updateLocationDto.NearbyLandmark,
            Street = updateLocationDto.Street
        };

        private static Location Map(CreateLocationDto createLocationDto) => new Location
        {
            BranchId = createLocationDto.BranchId,
            Area = createLocationDto.Area,
            Building = createLocationDto.Building,
            City = createLocationDto.City,
            Mall = createLocationDto.Mall,
            NearbyLandmark = createLocationDto.NearbyLandmark,
            Street = createLocationDto.Street
        };
    }
}
