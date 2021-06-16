using System;

namespace EmbraceQueue.Domain.Dtos.ServiceLines
{
    /// <summary>
    /// Object used to get an existing service line
    /// </summary>
    public class GetServiceLineDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Counter number
        /// </summary>
        public string CounterNumber { get; set; }

        /// <summary>
        /// Created at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// CurrentQueueStatus
        /// </summary>
        public int CurrentQueueStatus { get; set; }

        /// <summary>
        /// Current sequential number
        /// </summary>
        public int CurrentSequentialNumber { get; set; }

        /// <summary>
        /// Last incremented date time
        /// </summary>
        public DateTime LastIncrementedDateTime { get; set; }

        /// <summary>
        /// People got in line counter
        /// </summary>
        public int PeopleGotInLineCounter { get; set; }
    }
}
