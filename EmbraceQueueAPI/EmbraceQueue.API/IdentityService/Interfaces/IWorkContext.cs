using EmbraceQueue.Infrastructure.Entities;
using System.Threading.Tasks;

namespace EmbraceQueue.API.IdentityService.Interfaces
{
    /// <summary>
    /// Work context interface
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets current user
        /// </summary>
        /// <returns></returns>
        Task<User> GetCurrentUserAsync();
    }
}
