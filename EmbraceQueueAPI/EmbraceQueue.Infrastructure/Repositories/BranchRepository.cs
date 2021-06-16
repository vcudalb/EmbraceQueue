using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public BranchRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync()
        {
            return await _dbContext.Branches.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Branch> FindBranchByIdAsync(int id)
        {
            return await _dbContext.Branches.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Branch>> FindBranchesByIdAsync(int id)
        {
            return await _dbContext.Branches.AsNoTracking().Where(b => b.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateBranchAsync(Branch branch)
        {
            var existingBranch = await _dbContext.Branches.FirstOrDefaultAsync(c => c.Id == branch.Id).ConfigureAwait(false);

            if (existingBranch.CompanyId != branch.CompanyId && branch.CompanyId > 0) existingBranch.CompanyId = branch.CompanyId;
            if (existingBranch.WaitingTimeInSeconds != branch.WaitingTimeInSeconds && branch.WaitingTimeInSeconds != TimeSpan.Zero) existingBranch.WaitingTimeInSeconds = branch.WaitingTimeInSeconds;
            if (existingBranch.WorkDayStartTime != branch.WorkDayStartTime && branch.WorkDayStartTime != TimeSpan.Zero) existingBranch.WorkDayStartTime = branch.WorkDayStartTime;
            if (existingBranch.WorkDayEndTime != branch.WorkDayEndTime && branch.WorkDayEndTime != TimeSpan.Zero) existingBranch.WorkDayEndTime = branch.WorkDayEndTime;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Branch> AddBranchAsync(Branch branch)
        {
            await _dbContext.AddAsync(branch).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return branch;
        }

        public async Task DeleteBranchAsync(int id)
        {
            var existingBranch = await _dbContext.Branches.FirstOrDefaultAsync(b => b.Id == id).ConfigureAwait(false);
            _dbContext.Branches.Remove(existingBranch);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
