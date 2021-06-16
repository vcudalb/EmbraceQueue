using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public UserRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string id) => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
        public async Task<IEnumerable<IdentityUserRole<string>>> GetUserRolesAsync(string userId) => await _dbContext.UserRoles.AsNoTracking().Where(ur => ur.UserId == userId).ToListAsync().ConfigureAwait(false);
    }
}
