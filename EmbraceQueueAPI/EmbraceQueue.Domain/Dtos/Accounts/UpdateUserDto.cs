using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Accounts
{
    /// <summary>
    /// Object used to update an existing user
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Display(Name = "User name")]
        public string UserName { get; set; }

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
