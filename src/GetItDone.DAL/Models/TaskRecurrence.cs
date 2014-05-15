using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetItDone.DAL.Models
{
    public class TaskSchedule
    {
        [Key]
        public int TaskScheduleID { get; set; }
        
        /// <summary>
        /// The time between tasks in Days
        /// </summary>
        public int Schedule { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}
