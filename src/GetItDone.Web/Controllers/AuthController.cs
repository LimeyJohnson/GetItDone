﻿using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TrelloNet;

namespace GetItDone.Web.Controllers
{
    public class AuthController : Controller
    {
        GetItDoneContext db = new GetItDoneContext();
        // GET: /Auth/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email, Password")] User User)
        {

            var authedUser = (from u in db.Users where u.Email.Equals(User.Email, StringComparison.InvariantCultureIgnoreCase) select u).FirstOrDefault();
            //Check if we got any users
            if (authedUser!=null)
            {
                 //Check if the password is correct
                if (Crypto.VerifyHashedPassword(authedUser.Password, User.Password))
                {
                    Guid sessionGuid = CreateSession(authedUser, db);
                    //Set cookie for login
                    HttpCookie cookie = new HttpCookie("auth", sessionGuid.ToString());
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.AppendCookie(cookie);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.LoginMessage = "Invalid Password or Email";
            return View();

        }

        // GET: /Auth/
        public ActionResult Login()
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["callback_method"] = "postMessage";
            queryString["return_url"] = "http://localhost:22088/Auth/Login";
            queryString["scope"] = "read,write";
            queryString["expiration"] = "never";
            queryString["name"] = "Andrew's App";
            queryString["key"] = "e3da5185d8b29c21996f793a5dd0ef06";
            Debug.Print("QueryString = "+queryString);
            return Redirect("https://trello.com/1/authorize?"+queryString);
            
        }

        public ActionResult Logout()
        {
            try
            {
                Guid AuthGuid = CookieHelper.AuthCookie(Request);
                var session = (from s in db.Sessions where s.ID == AuthGuid select s).FirstOrDefault();
                if (session != null)
                {
                    db.Sessions.Remove(session);
                    db.SaveChanges();
                }
            }
            catch (ArgumentException) { }
            return RedirectToAction("Login");
        }
        //private string ClientID
        //{
        //    get
        //    {
        //        return WebConfigurationManager.AppSettings["MSClientID"];
        //    }
        //}
        private Guid CreateSession(User User, GetItDoneContext db)
        {
            Guid guid = Guid.NewGuid();
            db.Sessions.Add(new Session() { SessionUser = User, Created = DateTime.Now, Expires = DateTime.Now.AddMonths(1), ID = guid });
            db.SaveChanges();
            return guid;
        }

    }
}