using EmbraceQueue.API.IdentityService.Interfaces;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmbraceQueue.API.IdentityService
{
    /// <summary>
    /// Work context
    /// </summary>
    public class WorkContext : IWorkContext
    {
        private const string UserGuidCookiesName = "EmbraceQueueGuid";

        private User _currentUser;
        private UserManager<User> _userManager;
        private HttpContext _httpContext;
        private IUserRepository _userRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="userRepository"></param>
        public WorkContext(UserManager<User> userManager,
                           IHttpContextAccessor contextAccessor,
                           IUserRepository userRepository)
        {
            _userManager = userManager;
            _httpContext = contextAccessor.HttpContext;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetCurrentUserAsync()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            var contextUser = _httpContext.User;
            _currentUser = await _userManager.GetUserAsync(contextUser);

            if (_currentUser != null)
            {
                _currentUser = await _userRepository.GetUserAsync(_currentUser.Id).ConfigureAwait(false);
                return _currentUser;
            }

            return _currentUser;
        }
    }
}
