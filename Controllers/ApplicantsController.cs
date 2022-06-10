using System.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using online_recruitment.Models;
using System.Dynamic;

namespace online_recruitment.Controllers
{
	[Authorize(Roles = "Admin,HR Group")]
	public class ApplicantsController : Controller
	{
		private Entities db = new Entities();

		// GET: Applicants
		public ActionResult Index()
		{
			var applicants = db.Applicants.Include(a => a.ApplicantStatu).Include(a => a.Department);
			return View(applicants.ToList());
		}

		// GET: Applicants/Create
		public ActionResult Create()
		{
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
			return View();
		}

		// POST: Applicants/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Applicant applicant)
		{
			if (applicant.PdfFile.ContentLength > 0 && Path.GetExtension(applicant.PdfFile.FileName) == ".pdf")
			{
				string fileName = Path.GetFileName(applicant.PdfFile.FileName);
				string path = Path.Combine(Server.MapPath("~/UserCv"), Path.GetFileName(applicant.PdfFile.FileName));
				applicant.PdfFile.SaveAs(path);
				try
				{
					db.sp_CreateApplicant(applicant.Name, applicant.PhoneNumber, applicant.Email, applicant.DepartmentId, fileName);
                    TempData["Completed"] = "Successfully added a new Applicant";
                    return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ViewData["Error"] = ex.InnerException.Message;
					ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", applicant.DepartmentId);
					return View(applicant);

				}
			}
			ViewData["Error"] = "Please select a pdf file to upload";
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", applicant.DepartmentId);
			return View(applicant);
		}

		// GET: Applicants/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Applicant applicant = db.Applicants.Find(id);
			if (applicant == null)
			{
				return HttpNotFound();
			}
			ViewBag.Status = new SelectList(db.ApplicantStatus, "Id", "Name", applicant.Status);
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", applicant.DepartmentId);
			return View(applicant);
		}

		// POST: Applicants/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Applicant applicant)
		{
				try
				{
					db.sp_EditApplicant(applicant.Id, applicant.Name, applicant.PhoneNumber, applicant.Email,applicant.DepartmentId, applicant.Status);
                    TempData["Completed"] = "Successfully updated Applicant Details";
                    return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ViewData["Error"] = ex.InnerException.Message;
					ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", applicant.DepartmentId);
					return View(applicant);

				}
		}

		public ActionResult OpenCvFile (int? id) {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var data = db.Applicants.Find(id);

			string path = Path.Combine(Server.MapPath("~/UserCv"), data.CvFile);

			return File(path, "application/pdf");
		}

		public ActionResult BanApplicant(int? id) {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			db.sp_banApplicant(id);
			TempData["Completed"] = "The Applicant has been banned. All pending Interview are deemed rejected";
			return RedirectToAction("Index");
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AssignVacant data = new AssignVacant();
			data.applicant = db.Applicants.Find(id);
			data.vacancies = db.Vacancies.Where(x => x.DepartmentId == data.applicant.DepartmentId).Select(a => new VacancyDTO { VacancyId = a.Id, VacancyName = a.Name }).ToList();
			if (data.applicant == null)
			{
				return HttpNotFound();
			}
			return View(data);
		}

		public ActionResult AssignVacant(int? id, int vacant)
		{
			
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
