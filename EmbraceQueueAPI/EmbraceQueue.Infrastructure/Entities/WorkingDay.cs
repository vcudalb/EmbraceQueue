using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(BranchId), Name = "IX_WorkingDays_BranchId")]
    public partial class WorkingDay
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public int Day { get; set; }
        public int BranchId { get; set; }
        public TimeSpan BreakEndTime { get; set; }
        public TimeSpan BreakStartTime { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("WorkingDays")]
        public virtual Branch Branch { get; set; }
    }
}
