using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetItDone.Web.Controllers
{
    public class JobController : ApiController
    {
        private GetItDoneContext db = new GetItDoneContext();

        [HttpGet]
        public IHttpActionResult RecurringTasks()
        {
            List<TaskSchedule> schedules = (from s in db.Schedules.Include("Tasks").Include("Tasks.Owner") select s).ToList<TaskSchedule>();
            foreach (TaskSchedule schedule in schedules)
            {
                schedule.Tasks.Sort(new Comparison<Task>((t1, t2) => { return (t2.Created - t1.Created).Hours; }));
                Task latestChildTask = schedule.Tasks[0];
                if (latestChildTask == null || (DateTime.Now - latestChildTask.Created).TotalDays > schedule.Schedule)
                {
                    db.Entry(latestChildTask).State = System.Data.Entity.EntityState.Detached;
                    latestChildTask.Created = DateTime.Now;
                    latestChildTask.TaskID = 0;

                    db.Tasks.Add(latestChildTask);
                    db.SaveChanges();
                }
            }
            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
