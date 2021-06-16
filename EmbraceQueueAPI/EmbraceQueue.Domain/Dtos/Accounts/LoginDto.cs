using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Accounts
{
    /// <summary>
    /// Object used to login into system
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
