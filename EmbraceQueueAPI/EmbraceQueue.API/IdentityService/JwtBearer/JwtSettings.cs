using System;

namespace EmbraceQueue.API.IdentityService.JwtBearer
{
    /// <summary>
    /// JwtSettings
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// IsEnable
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// SecretKey
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// TokenPath
        /// </summary>
        public string TokenPath { get; set; }

        /// <summary>
        /// CookieName
        /// </summary>
        public string CookieName { get; set; }

        /// <summary>
        /// TokenLifetime
        /// </summary>
        public TimeSpan TokenLifetime { get; set; }
    }
}
