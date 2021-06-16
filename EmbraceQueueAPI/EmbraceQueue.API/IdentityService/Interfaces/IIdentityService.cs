using EmbraceQueue.API.IdentityService.Responses;
using System.Threading.Tasks;

namespace EmbraceQueue.API.IdentityService.Interfaces
{
    /// <summary>
    /// Identity service interface
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Login an existing user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        Task<AuthResult> LoginAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
