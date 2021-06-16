using System.Collections.Generic;

namespace EmbraceQueue.API.IdentityService.Responses
{
    /// <summary>
    /// Auth result
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Payload
        /// </summary>
        public AuthSuccessResponse Payload { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Errors
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
