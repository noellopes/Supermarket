﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class EmployeeSchedule
    {
        [Key]
        [Required]
        public int ScheduleId {  get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public TimeSpan LunchStartTime { get; set; }
        public TimeSpan LunchTime { get; set; }

    }
}