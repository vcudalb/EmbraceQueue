using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class ServicesServiceLineRepository : IServicesServiceLineRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public ServicesServiceLineRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServicesServiceLine> FindServicesServiceLine(int serviceId, int serviceLineId)
        {
            return await _dbContext.ServicesServiceLines.FirstOrDefaultAsync(ssl => ssl.ServiceId == serviceId && ssl.ServiceLineId == serviceLineId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ServicesServiceLine>> GetServicesServiceLinesAsync()
        {
            return await _dbContext.ServicesServiceLines.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<ServicesServiceLine>> FindAllServices(int serviceId)
        {
            return await _dbContext.ServicesServiceLines.AsNoTracking().Where(ssl => ssl.ServiceId == serviceId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<ServicesServiceLine>> FindAllServiceLines(int serviceLineId)
        {
            return await _dbContext.ServicesServiceLines.AsNoTracking().Where(ssl => ssl.ServiceLineId == serviceLineId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<ServicesServiceLine> AddServiceServiceLineAsync(ServicesServiceLine servicesServiceLine)
        {
            await _dbContext.AddAsync(servicesServiceLine).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return servicesServiceLine;
        }

        public async Task DeleteServiceServiceLineAsync(int serviceId, int serviceLineId)
        {
            var existingServicesServiceLine = await _dbContext.ServicesServiceLines.FirstOrDefaultAsync(ssl => ssl.ServiceId == serviceId && ssl.ServiceLineId == serviceLineId).ConfigureAwait(false);
            _dbContext.ServicesServiceLines.Remove(existingServicesServiceLine);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
