using GetItDone.DAL;
using GetItDone.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GetItDone.Web.Controllers
{
    public class BoardController : ApiController
    {
        private GetItDoneContext db = new GetItDoneContext();

        // GET api/Board/GetTaskList/id
        [HttpGet]
        public IHttpActionResult GetTaskList(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                db.Entry(user).Collection(u => u.Boards).Load();
                Board board = user.Boards.Where(b=>b.BoardID == id).FirstOrDefault<Board>();
                if(board != null)
                {
                    List<Task> returnList = (from t in db.Tasks where t.BoardID == board.BoardID select t).ToList<Task>();
                    return Ok(returnList);
                }
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public IHttpActionResult Update(Board board)
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
                postedBoard.Creator = user;
                user.Boards.Add(postedBoard);
                db.SaveChanges();
                return Ok(postedBoard);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
    }

}