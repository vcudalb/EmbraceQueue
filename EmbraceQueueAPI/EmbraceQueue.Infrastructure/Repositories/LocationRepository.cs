using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public LocationRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            return await _dbContext.Locations.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Location> FindLocationByIdAsync(int id)
        {
            return await _dbContext.Locations.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Location>> FindLocationsByIdAsync(int id)
        {
            return await _dbContext.Locations.AsNoTracking().Where(l => l.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateLocationAsync(Location location)
        {
            var existingLocation = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == location.Id).ConfigureAwait(false);

            if (existingLocation.BranchId != location.BranchId && location.BranchId > 0) existingLocation.BranchId = location.BranchId;
            if (existingLocation.Building != location.Building && location.Building.HasValue) existingLocation.Building = location.Building;
            if (existingLocation.Area != location.Area && !string.IsNullOrEmpty(location.Area)) existingLocation.Area = location.Area;
            if (existingLocation.City != location.City && !string.IsNullOrEmpty(location.City)) existingLocation.City = location.City;
            if (existingLocation.Mall != location.Mall && !string.IsNullOrEmpty(location.Mall)) existingLocation.Mall = location.Mall;
            if (existingLocation.NearbyLandmark != location.NearbyLandmark && !string.IsNullOrEmpty(location.NearbyLandmark)) existingLocation.NearbyLandmark = location.NearbyLandmark;
            if (existingLocation.Street != location.Street && !string.IsNullOrEmpty(location.Street)) existingLocation.Street = location.Street;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            var isAnExistingLocation = await _dbContext.Locations.AnyAsync(l => l.BranchId == location.BranchId).ConfigureAwait(false);
            if (isAnExistingLocation) throw new Exception($"Location with BranchId: {location.BranchId} already exists.");

            await _dbContext.AddAsync(location).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return location;
        }

        public async Task DeleteLocationAsync(int id)
        {
            var existingLocation = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
            _dbContext.Locations.Remove(existingLocation);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
