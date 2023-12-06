﻿using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Schedule
    {
        [Required]
        public int ScheduleID { get; set; }
        [Required(ErrorMessage = "The Start Date is mandatory.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The End Date is mandatory.")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "The Daily Start Date is mandatory.")]
        public DateTime DailyStartTime {  get; set; }
        [Required(ErrorMessage = "The Daily Finish Date is mandatory.")]
        public DateTime DailyFinishTime { get; set; }
        public int Id_Departments { get; set; }
        public Departments? Department { get; set; }
    }
}
