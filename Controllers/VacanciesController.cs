using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using online_recruitment.Models;

namespace online_recruitment.Controllers
{
	[Authorize(Roles = "Admin,HR Group")]
	public class VacanciesController : Controller
	{
		private Entities db = new Entities();

		// GET: Vacancies
		public ActionResult Index()
		{
			var vacancies = db.Vacancies.Include(v => v.Employee).Include(v => v.VacancyStatu);
			return View(vacancies.ToList());
		}

		// GET: Vacancies/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vacancy vacancy = db.Vacancies.Find(id);
			if (vacancy == null)
			{
				return HttpNotFound();
			}
			return View(vacancy);
		}

		// GET: Vacancies/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Vacancies/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Vacancy vacancy)
		{
			var current = db.Employees.Where(p => p.Username == User.Identity.Name).Single();
			DateTime ClosingDate = Convert.ToDateTime(vacancy.ClosingDate);
			try
			{
				
				db.sp_CreateVacancy(vacancy.Name, vacancy.Description, ClosingDate, vacancy.Quantity, current.Id,current.DepartmentId);
				TempData["Completed"] = "Successfully added new vacancies";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["Fail"] = ex.InnerException.Message;
				return View(vacancy);
			}

		}

		// GET: Vacancies/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vacancy vacancy = db.Vacancies.Find(id);
			if (vacancy == null)
			{
				return HttpNotFound();
			}
			return View(vacancy);
		}

		// POST: Vacancies/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Vacancy vacancy)
		{
			var current = db.Employees.Where(p => p.Username == User.Identity.Name).Single();
			DateTime ClosingDate = Convert.ToDateTime(vacancy.ClosingDate);
			try
			{
				db.sp_EditVacancy(vacancy.Id,vacancy.Name,vacancy.Description,ClosingDate,vacancy.Quantity,current.DepartmentId, vacancy.Status);
				TempData["Completed"] = "Successfully added new vacancies";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ViewData["Fail"] = ex.InnerException.Message;
				return View(vacancy);
			}

		}

		// GET: Vacancies/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vacancy vacancy = db.Vacancies.Find(id);
			if (vacancy == null)
			{
				return HttpNotFound();
			}
			return View(vacancy);
		}

		// POST: Vacancies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Vacancy vacancy = db.Vacancies.Find(id);
			db.Vacancies.Remove(vacancy);
			db.SaveChanges();
			return RedirectToAction("Index");
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
