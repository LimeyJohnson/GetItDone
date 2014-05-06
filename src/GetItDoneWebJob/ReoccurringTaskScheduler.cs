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
            GetItDoneContext db = new GetItDoneContext();
            List<Task> masterTasks = (from t in db.Tasks where t.ReoccurringSchedule != null select t).ToList<Task>();
            foreach(Task master in masterTasks)
            {
                Task latestChildTask = (from t in db.Tasks.Include("ReoccurringParent") where t.ReoccurringParent.TaskID == master.TaskID select t).OrderByDescending(t => t.Created).FirstOrDefault<Task>();
                if(latestChildTask == null || (DateTime.Now - latestChildTask.Created).TotalDays > master.ReoccurringSchedule)
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
