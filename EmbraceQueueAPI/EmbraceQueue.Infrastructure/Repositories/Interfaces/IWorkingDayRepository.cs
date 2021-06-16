using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IWorkingDayRepository
    {
        Task<IEnumerable<WorkingDay>> GetWorkingDaysAsync();
        Task<WorkingDay> FindWorkingDayByIdAsync(int id);
        Task<IEnumerable<WorkingDay>> FindWorkingDaysByIdAsync(int id);
        Task UpdateWorkingDayAsync(WorkingDay location);
        Task<WorkingDay> AddWorkingDayAsync(WorkingDay location);
        Task DeleteWorkingDayAsync(int id);
    }
}
