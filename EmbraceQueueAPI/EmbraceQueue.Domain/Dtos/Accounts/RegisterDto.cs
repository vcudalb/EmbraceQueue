using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Accounts
{
    /// <summary>
    /// Object used to register into system
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Role id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// IsSubscribed
        /// </summary>
        public bool IsSubscribed { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
