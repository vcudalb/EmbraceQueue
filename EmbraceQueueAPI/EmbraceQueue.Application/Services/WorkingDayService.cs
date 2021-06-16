using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.WorkingDays;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IWorkingDayRepository _workingDayRepository;

        public WorkingDayService(IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public async Task<IEnumerable<GetWorkingDayDto>> GetWorkingDaysAsync()
        {
            var workingDays = await _workingDayRepository.GetWorkingDaysAsync().ConfigureAwait(false);
            return workingDays.Select(x => Map(x));
        }

        public async Task<GetWorkingDayDto> FindWorkingDayByIdAsync(int id)
        {
            var workingDay = await _workingDayRepository.FindWorkingDayByIdAsync(id).ConfigureAwait(false);
            if (workingDay != null) return Map(workingDay);
            return null;
        }

        public async Task<IEnumerable<GetWorkingDayDto>> FindWorkingDaysByIdAsync(int id)
        {
            var workingDays = await _workingDayRepository.FindWorkingDaysByIdAsync(id).ConfigureAwait(false);
            return workingDays.Select(x => Map(x));
        }

        public async Task UpdateWorkingDayAsync(int id, UpdateWorkingDayDto workingDayDto)
        {
            await _workingDayRepository.UpdateWorkingDayAsync(Map(id, workingDayDto)).ConfigureAwait(false);
        }

        public async Task<GetWorkingDayDto> AddWorkingDayAsync(CreateWorkingDayDto workingDayDto)
        {
            var createdWorkingDay = await _workingDayRepository.AddWorkingDayAsync(Map(workingDayDto)).ConfigureAwait(false);
            return Map(createdWorkingDay);
        }

        public async Task DeleteWorkingDayAsync(int id)
        {
            await _workingDayRepository.DeleteWorkingDayAsync(id).ConfigureAwait(false);
        }

        private static GetWorkingDayDto Map(WorkingDay workingDay) => new GetWorkingDayDto
        {
            Id = workingDay.Id,
            BranchId = workingDay.BranchId,
            Day = workingDay.Day,
            DayStartTime = workingDay.DayStartTime,
            DayEndTime = workingDay.DayEndTime,
            BreakStartTime = workingDay.BreakStartTime,
            BreakEndTime = workingDay.BreakEndTime
        };

        private static WorkingDay Map(int id, UpdateWorkingDayDto updateWorkingDayDto) => new WorkingDay
        {
            Id = id,
            BranchId = updateWorkingDayDto.BranchId,
            Day = updateWorkingDayDto.Day,
            DayStartTime = updateWorkingDayDto.DayStartTime,
            DayEndTime = updateWorkingDayDto.DayEndTime,
            BreakStartTime = updateWorkingDayDto.BreakStartTime,
            BreakEndTime = updateWorkingDayDto.BreakEndTime
        };

        private static WorkingDay Map(CreateWorkingDayDto createWorkingDayDto) => new WorkingDay
        {
            BranchId = createWorkingDayDto.BranchId,
            Day = createWorkingDayDto.Day,
            DayStartTime = createWorkingDayDto.DayStartTime,
            DayEndTime = createWorkingDayDto.DayEndTime,
            BreakStartTime = createWorkingDayDto.BreakStartTime,
            BreakEndTime = createWorkingDayDto.BreakEndTime
        };
    }
}
