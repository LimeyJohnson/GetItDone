using GetItDone.DAL;
using GetItDone.DAL.Models;
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
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Tasks).Load();
                db.Entry(user).Collection(u=> u.Boards).Load();
                Task movedTask = user.Tasks.Find(t => t.TaskID == postedTask.TaskID);
                Board newBoard = user.Boards.Find(b=>b.BoardID == id);
                movedTask.ChangeBoard(newBoard);
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
        [HttpPost]
        public IHttpActionResult Update(Task postedTask)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            db.Entry(user).Collection(u => u.Tasks).Load();
            if (user != null)
            {
                Task editedTask = user.Tasks.Find(t => t.TaskID == postedTask.TaskID);
                db.Entry(editedTask).CurrentValues.SetValues(postedTask);
                db.SaveChanges();
                return Ok(editedTask);
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