using GetItDone.DAL;
using GetItDone.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GetItDone.Web.Controllers
{
    public static class CookieHelper
    {
        public static HttpCookie CreateSession(User user)
        {
            using (GetItDoneContext db = new GetItDoneContext())
            {
                Guid guid = Guid.NewGuid();
                db.Sessions.Add(new Session() { SessionUser = user, Created = DateTime.Now, Expires = DateTime.Now.AddMonths(1), ID = guid });
                HttpCookie cookie = new HttpCookie("auth", guid.ToString());
                cookie.Expires = DateTime.Now.AddMonths(1);
                return cookie;
            }
        }

        public static Guid AuthCookie(HttpRequestBase request)
        {
            if (request.Cookies["auth"] != null)
            {
                Guid cookieGuid = Guid.Parse(request.Cookies["auth"].Value);
                return cookieGuid;
            }
            throw new ArgumentNullException("Cookie did not exist");
        }

        //This version is for WebApi
        public static User LoggedInUser(HttpRequestMessage request, GetItDoneContext db = null/*We need this so that the user object is created in the correct context*/)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies("auth").FirstOrDefault();
            if (cookie != null)
            {
                Guid cookieGuid = Guid.Parse(cookie["auth"].Value);
                return LoggedInUser(cookieGuid, db);
            }
            return null;
        }
        //This version is for MVC
        public static User LoggedInUser(HttpRequestBase request, GetItDoneContext db = null/*We need this so that the user object is created in the correct context*/)
        {
            try
            {
                return LoggedInUser(AuthCookie(request), db);
            }
            catch (ArgumentNullException)
            {
                return null;
            }

        }
        private static User LoggedInUser(Guid cookieGuid, GetItDoneContext db)
        {
            db = db ?? new GetItDoneContext();
            //They have an auth cookie, now lets check if the session is still valid
            var session = (from s in db.Sessions.Include("SessionUser") where cookieGuid == s.ID select s).FirstOrDefault<Session>();
            if (session != null && session.Expires > DateTime.Now)
            {
                return session.SessionUser;
            }

            return null;
        }
    }
}