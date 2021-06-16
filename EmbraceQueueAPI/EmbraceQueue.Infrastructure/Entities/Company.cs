using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(CategoryId), Name = "IX_Companies_CategoryId")]
    public partial class Company
    {
        public Company()
        {
            Branches = new HashSet<Branch>();
            DigitalTickets = new HashSet<DigitalTicket>();
            Services = new HashSet<Service>();
        }

        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Companies")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(Branch.Company))]
        public virtual ICollection<Branch> Branches { get; set; }
        [InverseProperty(nameof(DigitalTicket.Company))]
        public virtual ICollection<DigitalTicket> DigitalTickets { get; set; }
        [InverseProperty(nameof(Service.Company))]
        public virtual ICollection<Service> Services { get; set; }
    }
}
