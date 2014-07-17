using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrelloNet;

namespace GetItDone.Web.Controllers
{
    public class HomeController : Controller
    {
        private GetItDoneContext db = new GetItDoneContext();
        // GET: /Home/
        public ActionResult Index()
        {
            //Get the authorized trello client
            Trello t = CookieHelper.GetTrello(Request);
            if (t != null)
            {
                return View();
            }
            
            return RedirectToAction("Login", "Auth");
        }
    }
}