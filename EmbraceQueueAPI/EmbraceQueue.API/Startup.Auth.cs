using System.Text;
using EmbraceQueue.API.IdentityService.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmbraceQueue.API
{
    /// <summary>
    /// Startup auth
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The token options
        /// </summary>
        public TokenProviderOptions _tokenProviderOptions;

        /// <summary>
        /// The SignInManager
        /// </summary>
        private IApplicationBuilder _app;

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        private void ConfigureAuth(IApplicationBuilder app)
        {
            _app = app;
            if (_jwtSettings.IsEnable)
            {
                _tokenProviderOptions = new TokenProviderOptions
                {
                    TokenPath = _jwtSettings.TokenPath,
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    Expiration = _jwtSettings.TokenLifetime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256)
                };

                app.UseMiddleware<TokenProviderMiddleware>(Options.Create(_tokenProviderOptions));
            }
        }
    }
}
