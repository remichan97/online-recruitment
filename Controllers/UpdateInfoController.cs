using System;
using online_recruitment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace online_recruitment.Controllers
{
	[Authorize]
	public class UpdateInfoController : Controller
	{
		private Entities db = new Entities();
		// GET: UpdateInfo
		public ActionResult Index()
		{
			var data = db.Employees.Where(e => e.Username == User.Identity.Name).Select(u => new UpdateInfo { FullName = u.FullName, Email = u.Email, PhoneNumber = u.PhoneNumber }).Single();
			return View(data);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateInfo dt)
        {
            int rs = db.sp_UpdateInfo(dt.FullName, dt.Email, dt.PhoneNumber, User.Identity.Name);

            if (rs == 0)
            {
                ViewData["Fail"] = "Someone else already has the email you wanted to use for your account! lease use another one!";
                return View("Index");
            }
            else
            {
                TempData["Completed"] = "Successfully updated your user information";
                return RedirectToAction("Index", "Home");
            }
        }
	}
}