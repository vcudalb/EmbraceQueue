using EmbraceQueue.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string id);
        Task<IEnumerable<IdentityUserRole<string>>> GetUserRolesAsync(string userId);
    }
}
