using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(DigitalTicketId), Name = "IX_Customers_DigitalTicketId")]
    [Index(nameof(ServiceLineId), Name = "IX_Customers_ServiceLineId")]
    public partial class Customer
    {
        [Key]
        public int Id { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string SequentialNumber { get; set; }
        [Column("HasReceivedSMSTicket")]
        public bool HasReceivedSmsticket { get; set; }
        [Column("HasReceivedSMSReminder")]
        public bool HasReceivedSmsreminder { get; set; }
        public bool HasShownUpAndGotServed { get; set; }
        public int ServiceLineId { get; set; }
        public int DigitalTicketId { get; set; }
        public DateTime PhoneNumberSubmissionDateTime { get; set; }
        public DateTime ServiceFinishDateTime { get; set; }

        [ForeignKey(nameof(DigitalTicketId))]
        [InverseProperty("Customers")]
        public virtual DigitalTicket DigitalTicket { get; set; }
        [ForeignKey(nameof(ServiceLineId))]
        [InverseProperty("Customers")]
        public virtual ServiceLine ServiceLine { get; set; }
    }
}
