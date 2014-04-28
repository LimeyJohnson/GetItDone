using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GetItDone.DAL.Models;
using GetItDone.DAL;
using System.Web.Helpers;

namespace GetItDone.Web.Controllers
{
    public class UserController : Controller
    {
        private GetItDoneContext db = new GetItDoneContext();

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create([Bind(Include="UserID,FirstName,LastName,Email,Phone,Password")] User user, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //Hash the password for goodness sake. Why does MVC not do this automatically
                user.Password = Crypto.HashPassword(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
            }
            Response.AppendCookie(CookieHelper.CreateSession(user));
                        
            return RedirectToAction("Index", "Home");
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
