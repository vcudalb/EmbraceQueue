using System;
using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Customers
{
    /// <summary>
    /// Object used to update an existing customer
    /// </summary>
    public class UpdateCustomerDto
    {
        /// <summary>
        /// Customer phone number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// Sequential number
        /// </summary>
        public string SequentialNumber { get; set; }

        /// <summary>
        /// Has received sms ticket
        /// </summary>
        [Required]
        public bool HasReceivedSmsticket { get; set; }

        /// <summary>
        /// Has received sms reminder
        /// </summary>
        [Required]
        public bool HasReceivedSmsreminder { get; set; }

        /// <summary>
        /// Has shown up and got served
        /// </summary>
        [Required]
        public bool HasShownUpAndGotServed { get; set; }

        /// <summary>
        /// Service line id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int ServiceLineId { get; set; }

        /// <summary>
        /// Digital ticket id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int DigitalTicketId { get; set; }

        /// <summary>
        /// Phone number submission date time
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2100")]
        public DateTime PhoneNumberSubmissionDateTime { get; set; }

        /// <summary>
        /// Service finish date time
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2100")]
        public DateTime ServiceFinishDateTime { get; set; }
    }
}
