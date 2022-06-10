using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using online_recruitment.Models;
using online_recruitment.Utils;
using System.Web.Mvc;

namespace online_recruitment.Controllers
{
    [AllowAnonymous]
    public class SignInController : Controller
    {
        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(Employee user)
        {
            using (Entities db = new Entities())
            {
                string pw = PasswordHash.GetSHA(user.Password);
                int count = db.Employees.Count(it => it.Username == user.Username && it.Password == pw);
                if (count > 0)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["warn"] = "Please double check your credentials";
                }
            }
            return View("Index");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "SignIn");
        }
    }
}