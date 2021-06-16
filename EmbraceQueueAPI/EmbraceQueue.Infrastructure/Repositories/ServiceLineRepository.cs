using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class ServiceLineRepository : IServiceLineRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public ServiceLineRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceLine>> GetServiceLinesAsync()
        {
            return await _dbContext.ServiceLines.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<ServiceLine> FindServiceLineByIdAsync(int id)
        {
            return await _dbContext.ServiceLines.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ServiceLine>> FindServiceLinesByIdAsync(int id)
        {
            return await _dbContext.ServiceLines.AsNoTracking().Where(s => s.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateServiceLineAsync(ServiceLine serviceLine)
        {
            var existingServiceLine = await _dbContext.ServiceLines.FirstOrDefaultAsync(s => s.Id == serviceLine.Id).ConfigureAwait(false);

            if (existingServiceLine.BranchId != serviceLine.BranchId && serviceLine.BranchId > 0) existingServiceLine.BranchId = serviceLine.BranchId;
            if (existingServiceLine.CounterNumber != serviceLine.CounterNumber && !string.IsNullOrEmpty(serviceLine.CounterNumber)) existingServiceLine.CounterNumber = serviceLine.CounterNumber;
            if (existingServiceLine.CreatedAt != serviceLine.CreatedAt && serviceLine.CreatedAt != DateTime.MinValue) existingServiceLine.CreatedAt = serviceLine.CreatedAt;
            if (existingServiceLine.CurrentQueueStatus != serviceLine.CurrentQueueStatus && serviceLine.CurrentQueueStatus >= 0) existingServiceLine.CurrentQueueStatus = serviceLine.CurrentQueueStatus;
            if (existingServiceLine.CurrentSequentialNumber != serviceLine.CurrentSequentialNumber && serviceLine.CurrentSequentialNumber >= 0) existingServiceLine.CurrentSequentialNumber = serviceLine.CurrentSequentialNumber;
            if (existingServiceLine.LastIncrementedDateTime != serviceLine.LastIncrementedDateTime && serviceLine.LastIncrementedDateTime != DateTime.MinValue) existingServiceLine.LastIncrementedDateTime = serviceLine.LastIncrementedDateTime;
            if (existingServiceLine.PeopleGotInLineCounter != serviceLine.PeopleGotInLineCounter && serviceLine.PeopleGotInLineCounter >= 0) existingServiceLine.PeopleGotInLineCounter = serviceLine.PeopleGotInLineCounter;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<ServiceLine> AddServiceLineAsync(ServiceLine serviceLine)
        {
            await _dbContext.AddAsync(serviceLine).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return serviceLine;
        }

        public async Task DeleteServiceLineAsync(int id)
        {
            var existingServiceLine = await _dbContext.ServiceLines.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            _dbContext.ServiceLines.Remove(existingServiceLine);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
