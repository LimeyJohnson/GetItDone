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
                Task latestChildTask = (from t in db.Tasks.Include("ReoccurringParent")
            }
        }
    }
}
