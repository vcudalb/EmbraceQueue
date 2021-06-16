using EmbraceQueue.API.IdentityService.Interfaces;
using EmbraceQueue.Domain.Dtos.Accounts;
using EmbraceQueue.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides account operations like (Login, Register and Logout)
    /// </summary>
    [Route("api/v{version:apiVersion}/accounts/")]
    public class AccountController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityService _identityService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        private const string SuperAdmin = "superadmin";
        private const string BranchManager = "branchmanager";
        private const string HelpDeskEmployee = "helpdeskemployee";
        private const string EndUser = "enduser";

        /// <summary>
        /// Default Account Controller constructor
        /// </summary>
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IIdentityService identityService,
            IWorkContext workContext,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityService = identityService;
            _workContext = workContext;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = GetUser(registerDto);
                    var result = await _userManager.CreateAsync(user, registerDto.Password);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(registerDto.RoleId))
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.RoleId);
                            if (!roleResult.Succeeded)
                            {
                                _logger.LogError($"Failed to add role: {registerDto.RoleId} to user: {user.Email}");

                                AddErrors(roleResult);
                                return BadRequest(roleResult);
                            }
                        }
                        _logger.LogInformation(3, "User created a new account with password.");

                        return Ok(user);
                    }
                    AddErrors(result);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _identityService.LoginAsync(loginDto.Email, loginDto.Password, false, lockoutOnFailure: false);
                    if (result.Success)
                    {
                        _logger.LogInformation(1, "User logged in.");
                        return Ok(result.Payload);
                    }
                    return Unauthorized(result.Errors);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Logout an existing user
        /// </summary>
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation(4, "User logged out at {0}.", DateTime.UtcNow);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var currentUser = await _workContext.GetCurrentUserAsync().ConfigureAwait(false);
                if (currentUser == null) return NotFound(new { Message = $"User from token with id {currentUser.Id} not found. Please provide a valid or id or contact administrator." });

                bool isAdmin = await _userManager.IsInRoleAsync(currentUser, SuperAdmin).ConfigureAwait(false);
                if (isAdmin)
                {
                    var allUsers = await _userManager.Users.ToListAsync().ConfigureAwait(false);
                    return Ok(allUsers);
                }

                bool isBranchManager = await _userManager.IsInRoleAsync(currentUser, BranchManager);
                if (isBranchManager)
                {
                    var allUsers = _userManager.Users.ToList().Where(u => IsHelpDeskEmployee(u).GetAwaiter().GetResult() || IsEndUser(u).GetAwaiter().GetResult()).ToList();
                    return Ok(allUsers);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <returns></returns>
        [HttpGet("find/{id}")]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<IActionResult> FindSingleUser(string id)
        {
            try
            {
                var currentUser = await _workContext.GetCurrentUserAsync().ConfigureAwait(false);
                if (currentUser == null) return NotFound(new { Message = $"User from token with id {currentUser.Id} not found. Please provide a valid or id or contact administrator." });

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
                if (user == null) return NotFound(new { Message = $"User with id {id} not found. Please provide a valid or id or contact administrator." });

                bool isAdmin = await _userManager.IsInRoleAsync(currentUser, SuperAdmin).ConfigureAwait(false);
                if (isAdmin) return Ok(user);

                bool isBranchManager = await _userManager.IsInRoleAsync(currentUser, BranchManager);
                if (isBranchManager)
                {
                    var isEndUser = await IsEndUser(user).ConfigureAwait(false);
                    var isHelpDeskEmployee = await IsHelpDeskEmployee(user).ConfigureAwait(false);
                    if (isEndUser || isHelpDeskEmployee) return Ok(user);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!IsRoleValid(createUserDto.RoleId)) return BadRequest(new { Message = $"Please provide a valid role id. Use examples bellow: {SuperAdmin}, {BranchManager}, {HelpDeskEmployee}, {EndUser}" });

                    var currentUser = await _workContext.GetCurrentUserAsync().ConfigureAwait(false);
                    if (currentUser == null) return NotFound(new { NotFound = $"User from token with id {currentUser.Id} not found. Please provide a valid or id or contact administrator." });

                    bool isAdmin = await _userManager.IsInRoleAsync(currentUser, SuperAdmin);
                    bool isBranchManager = await _userManager.IsInRoleAsync(currentUser, BranchManager);

                    if (!isAdmin && isBranchManager && createUserDto.RoleId.ToLower() != HelpDeskEmployee) return BadRequest(new { Message = "BranchManager can't add users with other roles then helpdeskemployee" });

                    var user = GetUser(createUserDto);
                    var result = await _userManager.CreateAsync(user, createUserDto.Password);
                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, createUserDto.RoleId);
                        if (!roleResult.Succeeded)
                        {
                            _logger.LogError($"Failed to add role: {createUserDto.RoleId} to user: {user.Email}");

                            AddErrors(roleResult);
                            return BadRequest(roleResult);
                        }
                        _logger.LogInformation(3, "User created a new account with password.");

                        return Ok(user);
                    }
                    AddErrors(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _workContext.GetCurrentUserAsync().ConfigureAwait(false);
                    if (currentUser == null) return NotFound(new { NotFound = $"User from token with id {currentUser.Id} not found. Please provide a valid or id or contact administrator." });

                    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
                    if (user == null) return NotFound(new { Message = $"User with id {id} not found. Please provide a valid or id or contact administrator." });

                    bool isAdmin = await _userManager.IsInRoleAsync(currentUser, SuperAdmin);
                    if (isAdmin)
                    {
                        SetUserValues(ref user, ref updateUserDto);

                        var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                        if (result.Succeeded) return Accepted();

                        return BadRequest(result.Errors);
                    }

                    bool isBranchManager = await _userManager.IsInRoleAsync(currentUser, BranchManager);
                    bool isUserHelpDeskEmployee = await IsHelpDeskEmployee(user);
                    if (isBranchManager && isUserHelpDeskEmployee)
                    {
                        SetUserValues(ref user, ref updateUserDto);

                        var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                        if (result.Succeeded) return Accepted();

                        return BadRequest(result.Errors);
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var currentUser = await _workContext.GetCurrentUserAsync().ConfigureAwait(false);
                if (currentUser == null) return NotFound(new { Message = $"User from token with id {currentUser.Id} not found. Please provide a valid or id or contact administrator." });

                bool isAdmin = await _userManager.IsInRoleAsync(currentUser, SuperAdmin).ConfigureAwait(false);
                if (isAdmin)
                {
                    var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
                    if (user == null) return NotFound(new { Message = $"User with id {id} not found. Please provide a valid or id or contact administrator." });

                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded) return Ok();
                    return BadRequest(result);
                }

                bool isBranchManager = await _userManager.IsInRoleAsync(currentUser, BranchManager);
                if (isBranchManager)
                {
                    var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
                    var isHelpDeskEmployee = await _userManager.IsInRoleAsync(currentUser, HelpDeskEmployee).ConfigureAwait(false);
                    if (isHelpDeskEmployee)
                    {
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded) return Ok();
                        return BadRequest(result);
                    }
                    return BadRequest(new { Message = "BranchManager can't delete users except with role Help Desk Employee" });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        private async Task<bool> IsHelpDeskEmployee(User user)
        {
            return await _userManager.IsInRoleAsync(user, HelpDeskEmployee).ConfigureAwait(false);
        }

        private async Task<bool> IsEndUser(User user)
        {
            return await _userManager.IsInRoleAsync(user, EndUser).ConfigureAwait(false);
        }

        private User GetUser(RegisterDto registerDto) => new User
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            IsSubscribed = registerDto.IsSubscribed,
            PhoneNumber = registerDto.PhoneNumber
        };

        private User GetUser(CreateUserDto createUserDto) => new User
        {
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            IsSubscribed = createUserDto.IsSubscribed,
            PhoneNumber = createUserDto.PhoneNumber
        };

        private bool IsRoleValid(string role) => role.ToLower() == SuperAdmin || role.ToLower() == BranchManager || role.ToLower() == HelpDeskEmployee || role.ToLower() == EndUser;

        private void SetUserValues(ref User user, ref UpdateUserDto model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.Equals(model.Email, user.Email, StringComparison.InvariantCultureIgnoreCase)) user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.UserName) && !string.Equals(model.UserName, user.UserName, StringComparison.InvariantCultureIgnoreCase)) user.UserName = model.UserName;
            if (!string.IsNullOrEmpty(model.FirstName) && !string.Equals(model.FirstName, user.FirstName, StringComparison.InvariantCultureIgnoreCase)) user.FirstName = model.FirstName;
            if (!string.IsNullOrEmpty(model.LastName) && !string.Equals(model.LastName, user.LastName, StringComparison.InvariantCultureIgnoreCase)) user.LastName = model.LastName;
            if (model.IsSubscribed != user.IsSubscribed) user.IsSubscribed = model.IsSubscribed;
            if (!string.IsNullOrEmpty(model.PhoneNumber) && !string.Equals(model.PhoneNumber, user.PhoneNumber, StringComparison.InvariantCultureIgnoreCase)) user.PhoneNumber = model.PhoneNumber;
        }

        #endregion
    }
}
