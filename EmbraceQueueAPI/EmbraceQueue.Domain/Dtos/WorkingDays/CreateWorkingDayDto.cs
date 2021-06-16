using System;
using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.WorkingDays
{
    /// <summary>
    /// Object used to create a new working day
    /// </summary>
    public class CreateWorkingDayDto
    {
        /// <summary>
        /// Day start time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan DayStartTime { get; set; }

        /// <summary>
        /// Day end time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan DayEndTime { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int Day { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int BranchId { get; set; }

        /// <summary>
        /// Break end time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan BreakEndTime { get; set; }

        /// <summary>
        /// Break start time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan BreakStartTime { get; set; }
    }
}
