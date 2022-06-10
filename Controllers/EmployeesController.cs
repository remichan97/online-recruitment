using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using online_recruitment.Models;
using online_recruitment.Utils;
using PagedList;

namespace online_recruitment.Controllers
{
	[Authorize(Roles = "Admin")]
	public class EmployeesController : Controller
	{
		private Entities db = new Entities();

		// GET: Employees
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
			var employees = db.Employees.Include(e => e.Department).Include(e => e.Role).Where(it => it.Id != 1);
			if (searchString != null)
			{
				page = 1;
			} else
			{
				searchString = currentFilter;
			}
			ViewBag.currentFilter = searchString;
			if (!String.IsNullOrEmpty(searchString))
			{
				employees = employees.Where(a => a.FullName.Contains(searchString) || a.Username.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "fullname_desc":
				employees = employees.OrderByDescending(a => a.FullName);
					break;
				default:
				employees = employees.OrderBy(a => a.FullName);
					break;
			}
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(employees.ToPagedList(pageNumber,pageSize));
		}

		// GET: Employees/Create
		public ActionResult Create()
		{
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
			ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
			return View();
		}

		// POST: Employees/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Username,Password,ConfirmPassword,DepartmentId,FullName,Email,PhoneNumber,RoleId")] Employee employee)
		{
			if (ModelState.IsValid)
			{
				string pw = PasswordHash.GetSHA(employee.Password);
				try
				{
					db.sp_CreateNewEmployee(employee.Username, pw, employee.DepartmentId, employee.FullName, employee.Email, employee.PhoneNumber, employee.RoleId);
				}
				catch (Exception e)
				{
					ViewData["Error"] = e.InnerException.Message;
					ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
					ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
					return View(employee);
				}
				TempData["Completed"] = "Successfully added new Employee";
				return RedirectToAction("Index");
			}
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
			ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
			return View();
		}

		// GET: Employees/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Employee employee = db.Employees.Find(id);
			if (employee == null)
			{
				return HttpNotFound();
			}
			ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
			ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
			return View(employee);
		}

		// POST: Employees/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Exclude = "Username, Password, ConfirmPassword")] Employee employee)
		{
			try
			{
				db.sp_EditEmployee(employee.Id, employee.DepartmentId, employee.FullName, employee.Email, employee.PhoneNumber, employee.RoleId);
				TempData["Completed"] = "Successfully Updated the Employee Information";
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				ViewData["Error"] = e.InnerException.Message;
				ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
				ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
				return View(employee);
			}
		}

		// GET: Employees/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Employee employee = db.Employees.Find(id);
			if (employee == null)
			{
				return HttpNotFound();
			}
			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Employee employee = db.Employees.Find(id);
			db.Employees.Remove(employee);
			db.SaveChanges();
			TempData["Completed"] = "The Delete request has been successfully completed";
			return RedirectToAction("Index");
		}

		// GET : ResetPassword
		[HttpGet]
		[ValidateAntiForgeryToken]
		public ActionResult ResetPassword(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			string pw = PasswordHash.GetSHA("123456789");
			
			TempData["Completed"] = "Successfully reset password for the selected user";
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
