using EmbraceQueue.API.IdentityService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EmbraceQueue.API.IdentityService.JwtBearer
{
    /// <summary>
    /// TokenProviderMiddleware
    /// </summary>
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly ILogger<TokenProviderMiddleware> _logger;

        /// <summary>
        /// TokenProviderMiddleware Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _options = options.Value;
            _logger = loggerFactory?.CreateLogger<TokenProviderMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory)); ;
            ThrowIfInvalidOptions(_options);
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_identityService"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context, IIdentityService _identityService)
        {
            _logger.LogInformation($"About to path {context.Request.Method} {context.Request.GetDisplayUrl()} request");
            return _next(context);
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }
        }
    }
}
