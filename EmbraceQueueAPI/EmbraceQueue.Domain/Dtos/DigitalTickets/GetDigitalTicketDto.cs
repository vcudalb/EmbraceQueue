namespace EmbraceQueue.Domain.Dtos.DigitalTickets
{
    /// <summary>
    /// Object used to get and existing digital ticket
    /// </summary>
    public class GetDigitalTicketDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

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
        public int NotificationsNumber { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
    }
}
