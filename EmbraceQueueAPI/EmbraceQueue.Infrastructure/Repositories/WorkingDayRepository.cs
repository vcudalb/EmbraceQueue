using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public WorkingDayRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WorkingDay>> GetWorkingDaysAsync()
        {
            return await _dbContext.WorkingDays.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<WorkingDay> FindWorkingDayByIdAsync(int id)
        {
            return await _dbContext.WorkingDays.AsNoTracking().FirstOrDefaultAsync(wd => wd.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<WorkingDay>> FindWorkingDaysByIdAsync(int id)
        {
            return await _dbContext.WorkingDays.AsNoTracking().Where(wd => wd.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateWorkingDayAsync(WorkingDay workingDay)
        {
            var existingWorkingDay = await _dbContext.WorkingDays.FirstOrDefaultAsync(wd => wd.Id == workingDay.Id).ConfigureAwait(false);

            if (existingWorkingDay.BranchId != workingDay.BranchId && workingDay.BranchId > 0) existingWorkingDay.BranchId = workingDay.BranchId;
            if (existingWorkingDay.Day != workingDay.Day) existingWorkingDay.Day = workingDay.Day;
            if (existingWorkingDay.DayStartTime != workingDay.DayStartTime && workingDay.DayStartTime != TimeSpan.Zero) existingWorkingDay.DayStartTime = workingDay.DayStartTime;
            if (existingWorkingDay.DayEndTime != workingDay.DayEndTime && workingDay.DayEndTime != TimeSpan.Zero) existingWorkingDay.DayEndTime = workingDay.DayEndTime;
            if (existingWorkingDay.BreakStartTime != workingDay.BreakStartTime && workingDay.BreakStartTime != TimeSpan.Zero) existingWorkingDay.BreakStartTime = workingDay.BreakStartTime;
            if (existingWorkingDay.BreakEndTime != workingDay.BreakEndTime && workingDay.BreakEndTime != TimeSpan.Zero) existingWorkingDay.BreakEndTime = workingDay.BreakEndTime;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<WorkingDay> AddWorkingDayAsync(WorkingDay workingDay)
        {
            await _dbContext.AddAsync(workingDay).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return workingDay;
        }

        public async Task DeleteWorkingDayAsync(int id)
        {
            var existingWorkingDay = await _dbContext.WorkingDays.FirstOrDefaultAsync(wd => wd.Id == id).ConfigureAwait(false);
            _dbContext.WorkingDays.Remove(existingWorkingDay);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
