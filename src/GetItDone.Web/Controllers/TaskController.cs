using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace GetItDone.Web.Controllers
{
    public class TaskController : ApiController
    {
        private GetItDoneContext db = new GetItDoneContext();


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
            if (postedTask == null || postedTask.TaskID == 0) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                Task movedTask = (from t in db.Tasks.Include("Board") where t.TaskID == postedTask.TaskID select t).FirstOrDefault<Task>();
                //We need to verify that they have access to the board that the task came from, and we need to verify they have access to the board it came from

                Board newBoard = user.Boards.Find(b => b.BoardID == id);
                if (newBoard != null && user.Boards.Contains(movedTask.Board))
                {
                    movedTask.ChangeBoard(newBoard);
                    db.SaveChanges();
                    return Ok(movedTask);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            return StatusCode(HttpStatusCode.Unauthorized);
        }
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {

                Task deletedTask = (from t in db.Tasks.Include("Board") where t.TaskID == id select t).FirstOrDefault<Task>();
                if (user.Boards.Contains(deletedTask.Board))
                {
                    db.Tasks.Remove(deletedTask);
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public IHttpActionResult NewTask(Task postedTask, int id)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                postedTask.Board = (from c in db.Boards.Include("Tasks") where c.BoardID == id select c).FirstOrDefault<Board>();
                //This request is to add a new task. New tasks can only be added to the backlog at the lowest priority
                int? maxPriority = postedTask.Board.Tasks.Max(t => t.Priority);
                if (maxPriority.HasValue)
                {
                    postedTask.Priority = maxPriority.Value + 1;
                }
                postedTask.Created = DateTime.Now;
                postedTask.Creator = user;
                //Add in the board
                db.Tasks.Add(postedTask);
                db.SaveChanges();
                return Ok(postedTask);
            }
            return StatusCode(HttpStatusCode.BadRequest);

        }
        [HttpPost]
        public IHttpActionResult Update(Task postedTask)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            
            if (user != null)
            {
                Task editedTask = (from t in db.Tasks.Include("Board") where t.TaskID == postedTask.TaskID select t).FirstOrDefault<Task>();
                if (user.Boards.Contains(editedTask.Board))
                {
                    db.Entry(editedTask).CurrentValues.SetValues(postedTask);
                    db.SaveChanges();
                    return Ok(editedTask);
                }
                return StatusCode(HttpStatusCode.Unauthorized);
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