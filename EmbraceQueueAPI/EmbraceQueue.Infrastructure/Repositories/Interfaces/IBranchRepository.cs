using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetBranchesAsync();
        Task<Branch> FindBranchByIdAsync(int id);
        Task<IEnumerable<Branch>> FindBranchesByIdAsync(int id);
        Task UpdateBranchAsync(Branch branch);
        Task<Branch> AddBranchAsync(Branch branch);
        Task DeleteBranchAsync(int id);
    }
}
