using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GetItDone.DAL.Models;
using GetItDone.DAL;


namespace GetItDone.Web.Controllers
{
    public class TaskController : ApiController
    {
        private GetItDoneContext db = new GetItDoneContext();

        /*
         * This gets all of the tasks that should be shown on the home screen. 
         * That is to say all tasks whose status is backlog, inprogress, or done
         */
        // GET api/Task/GetTaskList/id
        [HttpGet]
        public IEnumerable<Task> GetTaskList(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {

                var userTasks = (from u in db.Users.Include(u => u.Tasks.Select(t => t.Board)) where u.UserID == user.UserID select u).FirstOrDefault<User>();
                return userTasks.Tasks.Where(t => t.Board.BoardID == id).OrderBy(t => t.Priority);
            }
            return null;
        }
        [HttpGet]
        public List<Board> Boards()
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                return user.Boards;
            }
            return null;
        }
        /* This method has 3 uses 
         * 1. If the task does not exist in the database it needs to be added
         * 2. If the task exists, but the status has changed then this is an update status request
         * 3. If the task exists with the same status then this is to reprioritize
         */

        [HttpPost]
        public IHttpActionResult MoveTask(Task postedTask, int id)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Tasks).Load();
                db.Entry(user).Collection(u=> u.Boards).Load();
                Task movedTask = user.Tasks.Find(t => t.TaskID == postedTask.TaskID);
                Board newBoard = user.Boards.Find(b=>b.BoardID == id);
                movedTask.Board = newBoard;
                db.SaveChanges();
                return Ok(movedTask);
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Tasks).Load();
                Task deletedTask = user.Tasks.Find(t => t.TaskID == id);
                db.Tasks.Remove(deletedTask);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public IHttpActionResult NewTask(Task postedTask, int id)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            db.Entry(user).Collection(u => u.Tasks).Load();
            if (user != null)
            {
                //This request is to add a new task. New tasks can only be added to the backlog at the lowest priority
                int? maxPriority = user.Tasks.Max(t => t.Priority);
                if (maxPriority.HasValue)
                {
                    postedTask.Priority = maxPriority.Value + 1;
                }

                //Add in the board
                postedTask.Board = (from c in db.Boards where c.BoardID == id select c).FirstOrDefault<Board>();
                user.Tasks.Add(postedTask);
                db.SaveChanges();
                return Ok(postedTask);
            }
            return StatusCode(HttpStatusCode.BadRequest);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}