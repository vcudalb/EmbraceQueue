using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(BranchId), Name = "IX_ServiceLines_BranchId")]
    public partial class ServiceLine
    {
        public ServiceLine()
        {
            Customers = new HashSet<Customer>();
            ServicesServiceLines = new HashSet<ServicesServiceLine>();
        }

        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string CounterNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CurrentQueueStatus { get; set; }
        public int CurrentSequentialNumber { get; set; }
        public DateTime LastIncrementedDateTime { get; set; }
        public int PeopleGotInLineCounter { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("ServiceLines")]
        public virtual Branch Branch { get; set; }
        [InverseProperty(nameof(Customer.ServiceLine))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(ServicesServiceLine.ServiceLine))]
        public virtual ICollection<ServicesServiceLine> ServicesServiceLines { get; set; }
    }
}
