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

        public Task RecurringTask{ get; set; }

        public Schedules Schedule { get; set; }

        public Board InitialBoard { get; set; }
    }
}
