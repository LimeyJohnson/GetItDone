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
        public IEnumerable<Task> GetTaskList(int id)
        {
            User user = CookieHelper.LoggedInUser(Request, db);
            if (user != null)
            {
                var userTasks = (from u in db.Users.Include("Tasks").Include("Tasks.Board") where u.UserID == user.UserID select u).FirstOrDefault<User>();
                return userTasks.Tasks.Where(t => t.Board.BoardID == id).Where(t=>t.Visible).OrderBy(t => t.Priority);
            }
            return null;
        }
    }
}