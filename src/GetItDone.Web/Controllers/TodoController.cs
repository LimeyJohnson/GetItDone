using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace GetItDone.Web.Controllers
{
    public class TodoController : ApiController
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
                db.Entry(user).Collection(u => u.Boards).Load();
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
                db.Entry(user).Collection(u => u.Boards).Load();
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
        public IHttpActionResult UpdateTask(Task postedTask)
        {
            if (postedTask == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            
            if (user != null)
            {
                Task editedTask = (from t in db.Tasks.Include("Board") where t.TaskID == postedTask.TaskID select t).FirstOrDefault<Task>();
                db.Entry(user).Collection(u => u.Boards).Load();
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

        #region Board Apis
        // GET api/Board/GetTaskList/id
        [HttpGet]
        public IHttpActionResult GetTaskList(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                Board board = user.Boards.Where(b => b.BoardID == id).FirstOrDefault<Board>();
                if (board != null)
                {
                    List<Task> returnList = (from t in db.Tasks where t.BoardID == board.BoardID select t).ToList<Task>();
                    return Ok(returnList.Where(t=>t.Visible));
                }
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public IHttpActionResult UpdateBoard(Board board)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                Board existingBoard = user.Boards.Find(b => b.BoardID == board.BoardID);
                if (existingBoard == null) { return StatusCode(HttpStatusCode.BadRequest); }
                db.Entry(existingBoard).CurrentValues.SetValues(board);
                db.SaveChanges();
                return Ok(existingBoard);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public IHttpActionResult DeleteBoard(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                Board deletedBoard = user.Boards.Find(b => b.BoardID == id);
                db.Entry(deletedBoard).Collection(b => b.Tasks).Load();
                if (deletedBoard.Tasks.Count == 0)
                {
                    db.Boards.Remove(deletedBoard);
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public IHttpActionResult NewBoard(Board postedBoard)
        {
            if (postedBoard == null) return StatusCode(HttpStatusCode.BadRequest);
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                user.Boards.Add(postedBoard);
                db.SaveChanges();
                return Ok(postedBoard);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        #endregion

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