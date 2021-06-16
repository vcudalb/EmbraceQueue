using System;

namespace EmbraceQueue.Domain.Dtos.Customers
{
    /// <summary>
    /// Object used to get an existing customer
    /// </summary>
    public class GetCustomerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

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
        public bool HasReceivedSmsticket { get; set; }

        /// <summary>
        /// Has received sms reminder
        /// </summary>
        public bool HasReceivedSmsreminder { get; set; }

        /// <summary>
        /// Has shown up and got served
        /// </summary>
        public bool HasShownUpAndGotServed { get; set; }

        /// <summary>
        /// Service line id
        /// </summary>
        public int ServiceLineId { get; set; }

        /// <summary>
        /// Digital ticket id
        /// </summary>
        public int DigitalTicketId { get; set; }

        /// <summary>
        /// Phone number submission date time
        /// </summary>
        public DateTime PhoneNumberSubmissionDateTime { get; set; }

        /// <summary>
        /// Service finish date time
        /// </summary>
        public DateTime ServiceFinishDateTime { get; set; }
    }
}
