using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Branches;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<GetBranchDto>> GetBranchesAsync()
        {
            var branches = await _branchRepository.GetBranchesAsync().ConfigureAwait(false);
            return branches.Select(x => Map(x));
        }

        public async Task<GetBranchDto> FindBranchByIdAsync(int id)
        {
            var branch = await _branchRepository.FindBranchByIdAsync(id).ConfigureAwait(false);
            if (branch != null) return Map(branch);
            return null;
        }

        public async Task<IEnumerable<GetBranchDto>> FindBranchesByIdAsync(int id)
        {
            var branches = await _branchRepository.FindBranchesByIdAsync(id).ConfigureAwait(false);
            return branches.Select(x => Map(x));
        }

        public async Task UpdateBranchAsync(int id, UpdateBranchDto branch)
        {
            await _branchRepository.UpdateBranchAsync(Map(id, branch)).ConfigureAwait(false);
        }

        public async Task<GetBranchDto> AddBranchAsync(CreateBranchDto branch)
        {
            var createdBranch = await _branchRepository.AddBranchAsync(Map(branch)).ConfigureAwait(false);
            return Map(createdBranch);
        }

        public async Task DeleteBranchAsync(int id)
        {
            await _branchRepository.DeleteBranchAsync(id).ConfigureAwait(false);
        }

        private static GetBranchDto Map(Branch branch) => new GetBranchDto
        {
            Id = branch.Id,
            CompanyId = branch.CompanyId,
            WaitingTimeInSeconds = branch.WaitingTimeInSeconds,
            WorkDayStartTime = branch.WorkDayStartTime,
            WorkDayEndTime = branch.WorkDayEndTime
        };

        private static Branch Map(int id, UpdateBranchDto updateBranchDto) => new Branch
        {
            Id = id,
            CompanyId = updateBranchDto.CompanyId,
            WaitingTimeInSeconds = updateBranchDto.WaitingTimeInSeconds,
            WorkDayStartTime = updateBranchDto.WorkDayStartTime,
            WorkDayEndTime = updateBranchDto.WorkDayEndTime
        };

        private static Branch Map(CreateBranchDto createBranchDto) => new Branch
        {
            CompanyId = createBranchDto.CompanyId,
            WaitingTimeInSeconds = createBranchDto.WaitingTimeInSeconds,
            WorkDayStartTime = createBranchDto.WorkDayStartTime,
            WorkDayEndTime = createBranchDto.WorkDayEndTime
        };
    }
}
