using System;

namespace EmbraceQueue.Domain.Dtos.Branches
{
    /// <summary>
    /// Object used to get an existing branch
    /// </summary>
    public class GetBranchDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Waiting time in seconds
        /// </summary>
        public TimeSpan WaitingTimeInSeconds { get; set; }

        /// <summary>
        /// Work days end time
        /// </summary>
        public TimeSpan WorkDayEndTime { get; set; }

        /// <summary>
        /// Work day start time
        /// </summary>
        public TimeSpan WorkDayStartTime { get; set; }
    }
}
