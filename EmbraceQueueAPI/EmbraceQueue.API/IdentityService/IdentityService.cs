using EmbraceQueue.API.IdentityService.Interfaces;
using EmbraceQueue.API.IdentityService.JwtBearer;
using EmbraceQueue.Infrastructure.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using EmbraceQueue.API.IdentityService.Responses;

namespace EmbraceQueue.API.IdentityService
{
    /// <summary>
    /// Identity service
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Identity Service default constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="jwtSettings"></param>
        public IdentityService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public async Task<AuthResult> LoginAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var identity = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
                if (identity.IsLockedOut)
                {
                    return new AuthResult
                    {
                        Errors = new[] { "This user is locked." }
                    };
                }

                if (identity.Succeeded)
                {
                    var response = await GenerateAuthenticationResultForUserAsync(user);

                    return new AuthResult { Payload = response, Success = true };
                }
            }

            return new AuthResult { Errors = new[] { "Invalid username or password." } };
        }

        private async Task<AuthSuccessResponse> GenerateAuthenticationResultForUserAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            // Get valid claims and pass them into JWT
            var claims = GetValidClaimsIdentity(user);

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthSuccessResponse
            {
                AccessToken = tokenHandler.WriteToken(token),
            };
        }

        private List<Claim> GetValidClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return claims;
        }
    }
}
