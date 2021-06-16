using System;
using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.ServiceLines
{
    /// <summary>
    /// Object used to update an existing service line
    /// </summary>
    public class UpdateServiceLineDto
    {
        /// <summary>
        /// Branch id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int BranchId { get; set; }

        /// <summary>
        /// Counter number
        /// </summary>
        public string CounterNumber { get; set; }

        /// <summary>
        /// Created at
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2100")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// CurrentQueueStatus
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int CurrentQueueStatus { get; set; }

        /// <summary>
        /// Current sequential number
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int CurrentSequentialNumber { get; set; }

        /// <summary>
        /// Last incremented date time
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2100")]
        public DateTime LastIncrementedDateTime { get; set; }

        /// <summary>
        /// People got in line counter
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int PeopleGotInLineCounter { get; set; }
    }
}
