using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetItDone.Web.Controllers
{
    public class HomeController : Controller
    {
        private GetItDoneContext db = new GetItDoneContext();
        // GET: /Home/
        public ActionResult Index()
        {

            //Check that the are logged in, if they are not redirect to login
            User user = CookieHelper.LoggedInUser(Request, db);
            if(user != null)
            {
                //Make sure catagories are loaded
                if (user.Boards == null) 
                {
                    db.Entry(user).Collection(u => u.Boards).Load();
                }
                return View(user);
            }
            
            return RedirectToAction("Login", "Auth");
        }
    }
}