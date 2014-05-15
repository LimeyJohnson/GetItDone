using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetItDoneWebJob
{
    internal static class ReocurringTaskScheduler
    {
        internal static void Schedule()
        {
            GetItDoneContext db = new GetItDoneContext("GetItDone");
            List<TaskSchedule> schedules = (from s in db.Schedules.Include("Task") select s).ToList<TaskSchedule>();
            foreach(TaskSchedule schedule in schedules)
            {
                schedule.Tasks.Sort(new Comparison<Task>((t1,t2)=>{return (t1.Created - t2.Created).Hours;}));
                Task latestChildTask = schedule.Tasks[0];
                if(latestChildTask == null || (DateTime.Now - latestChildTask.Created).TotalDays > schedule.Schedule)
                {
                    db.Entry(latestChildTask).State = System.Data.Entity.EntityState.Detached;
                    latestChildTask.Created = DateTime.Now;
                    db.Tasks.Add(latestChildTask);
                    db.SaveChanges();
                }
             }
        }
    }
}
