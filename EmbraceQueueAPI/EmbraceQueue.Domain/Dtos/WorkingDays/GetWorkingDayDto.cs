using System;

namespace EmbraceQueue.Domain.Dtos.WorkingDays
{
    /// <summary>
    /// Object used to get an existing working day
    /// </summary>
    public class GetWorkingDayDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Day start time
        /// </summary>
        public TimeSpan DayStartTime { get; set; }

        /// <summary>
        /// Day end time
        /// </summary>
        public TimeSpan DayEndTime { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Break end time
        /// </summary>
        public TimeSpan BreakEndTime { get; set; }

        /// <summary>
        /// Break start time
        /// </summary>
        public TimeSpan BreakStartTime { get; set; }
    }
}
