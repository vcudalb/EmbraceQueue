using EmbraceQueue.Domain.Dtos.WorkingDays;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IWorkingDayService
    {
        Task<IEnumerable<GetWorkingDayDto>> GetWorkingDaysAsync();
        Task<GetWorkingDayDto> FindWorkingDayByIdAsync(int id);
        Task<IEnumerable<GetWorkingDayDto>> FindWorkingDaysByIdAsync(int id);
        Task UpdateWorkingDayAsync(int id, UpdateWorkingDayDto workingDay);
        Task<GetWorkingDayDto> AddWorkingDayAsync(CreateWorkingDayDto workingDay);
        Task DeleteWorkingDayAsync(int id);
    }
}
