using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(CompanyId), Name = "IX_Branches_CompanyId")]
    public partial class Branch
    {
        public Branch()
        {
            WorkingDays = new HashSet<WorkingDay>();
            ServiceLines = new HashSet<ServiceLine>();
        }

        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public TimeSpan WaitingTimeInSeconds { get; set; }
        public TimeSpan WorkDayEndTime { get; set; }
        public TimeSpan WorkDayStartTime { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Branches")]
        public virtual Company Company { get; set; }
        [InverseProperty("Branch")]
        public virtual Location Location { get; set; }
        [InverseProperty(nameof(WorkingDay.Branch))]
        public virtual ICollection<WorkingDay> WorkingDays { get; set; }
        [InverseProperty(nameof(ServiceLine.Branch))]
        public virtual ICollection<ServiceLine> ServiceLines { get; set; }
    }
}
