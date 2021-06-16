using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.DigitalTickets
{
    /// <summary>
    /// Object used to create a new digital ticket
    /// </summary>
    public class CreateDigitalTicketDto
    {
        /// <summary>
        /// Message template one
        /// </summary>
        public string MessageTemplateOne { get; set; }

        /// <summary>
        /// Message template two
        /// </summary>
        public string MessageTemplateTwo { get; set; }

        /// <summary>
        /// Notifications number
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int NotificationsNumber { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int CompanyId { get; set; }
    }
}
