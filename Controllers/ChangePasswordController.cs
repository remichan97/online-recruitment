using online_recruitment.Models;
using online_recruitment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace online_recruitment.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private Entities db = new Entities();
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword chg)
        {
            string username = User.Identity.Name;
            if (chg.OldPassword == chg.NewPassword)
            {
                ViewData["Error"] = "You cannot reuse old password when changing your password";
                return View("Index");
            }
            string oldPwd = PasswordHash.GetSHA(chg.OldPassword);
            string newPwd = PasswordHash.GetSHA(chg.NewPassword);
            int cnt = db.sp_ChangePassword(oldPwd, newPwd, username);
            if (cnt == 0)
            {
                ViewData["Error"] = "Incorrect Password! Please try again";
                return View("Index");
            } else
            {
                FormsAuthentication.SignOut();
                TempData["Change"] = "Your Password has been successfully changed. Please Sign-in again";
                return RedirectToAction("Index", "SignIn");
            }
        }
    }
}