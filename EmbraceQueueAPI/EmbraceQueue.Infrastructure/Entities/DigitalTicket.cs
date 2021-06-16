using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(CompanyId), Name = "IX_DigitalTickets_CompanyId")]
    public partial class DigitalTicket
    {
        public DigitalTicket()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        public int Id { get; set; }
        public string MessageTemplateOne { get; set; }
        public string MessageTemplateTwo { get; set; }
        public int NotificationsNumber { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("DigitalTickets")]
        public virtual Company Company { get; set; }
        [InverseProperty(nameof(Customer.DigitalTicket))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
