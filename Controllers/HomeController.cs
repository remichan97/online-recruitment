using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace online_recruitment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Departments");
            }
            else if (User.IsInRole("HR Group"))
            {
                return RedirectToAction("Index", "Vacancies");
            }
            return RedirectToAction("Index", "Interviews");

        }
    }
}