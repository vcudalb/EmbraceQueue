using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public ServiceRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _dbContext.Services.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Service> FindServiceByIdAsync(int id)
        {
            return await _dbContext.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Service>> FindServicesByIdAsync(int id)
        {
            return await _dbContext.Services.AsNoTracking().Where(s => s.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateServiceAsync(Service service)
        {
            var existingService = await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == service.Id).ConfigureAwait(false);

            if (existingService.CompanyId != service.CompanyId && service.CompanyId > 0) existingService.CompanyId = service.CompanyId;
            if (existingService.ServiceType != service.ServiceType && !string.IsNullOrEmpty(service.ServiceType)) existingService.ServiceType = service.ServiceType;
            if (existingService.RecentIncrementedLineId != service.RecentIncrementedLineId && service.RecentIncrementedLineId > 0) existingService.RecentIncrementedLineId = service.RecentIncrementedLineId;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Service> AddServiceAsync(Service service)
        {
            await _dbContext.AddAsync(service).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return service;
        }

        public async Task DeleteServiceAsync(int id)
        {
            var existingService = await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            _dbContext.Services.Remove(existingService);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
