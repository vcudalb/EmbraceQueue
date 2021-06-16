using EmbraceQueue.Domain.Dtos.Branches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<GetBranchDto>> GetBranchesAsync();
        Task<GetBranchDto> FindBranchByIdAsync(int id);
        Task<IEnumerable<GetBranchDto>> FindBranchesByIdAsync(int id);
        Task UpdateBranchAsync(int id, UpdateBranchDto branch);
        Task<GetBranchDto> AddBranchAsync(CreateBranchDto branch);
        Task DeleteBranchAsync(int id);
    }
}
