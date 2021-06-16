using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmbraceQueue.API.IdentityService.JwtBearer
{
    /// <summary>
    /// Custom jwt data formater
    /// </summary>
    public class CustomJwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string algorithm;
        private readonly TokenValidationParameters validationParameters;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="validationParameters"></param>
        public CustomJwtDataFormat(string algorithm, TokenValidationParameters validationParameters)
        {
            this.algorithm = algorithm;
            this.validationParameters = validationParameters;
        }

        /// <summary>
        /// Unproctected
        /// </summary>
        /// <param name="protectedText"></param>
        /// <returns></returns>
        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        /// <summary>
        /// Unproctected
        /// </summary>
        /// <param name="protectedText"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = null;
            SecurityToken validToken = null;

            try
            {
                principal = handler.ValidateToken(protectedText, this.validationParameters, out validToken);

                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                {
                    throw new ArgumentException("Invalid JWT");
                }

                if (!validJwt.Header.Alg.Equals(algorithm, StringComparison.Ordinal))
                {
                    throw new ArgumentException($"Algorithm must be '{algorithm}'");
                }

            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Token validation passed
            return new AuthenticationTicket(principal, new AuthenticationProperties(), "Cookie");
        }

        /// <summary>
        /// Protect
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Protect
        /// </summary>
        /// <param name="data"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException();
        }
    }
}
