using System;
using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Branches
{
    /// <summary>
    /// Object used to create a new branch
    /// </summary>
    public class CreateBranchDto
    {
        /// <summary>
        /// Company id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Waiting time in seconds
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan WaitingTimeInSeconds { get; set; }

        /// <summary>
        /// Work days end time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan WorkDayEndTime { get; set; }

        /// <summary>
        /// Work day start time
        /// </summary>
        [Required]
        [Range(typeof(TimeSpan), "00:00:01", "23:59:59")]
        public TimeSpan WorkDayStartTime { get; set; }
    }
}
