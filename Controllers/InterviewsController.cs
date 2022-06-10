using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using online_recruitment.Models;
using online_recruitment.Utils;

namespace online_recruitment.Controllers
{
    [Authorize(Roles = "Admin,Interviewer")]
    public class InterviewsController : Controller
    {
        private Entities db = new Entities();

        // GET: Interviews
        public ActionResult Index()
        {
            var interviews = db.Interviews.Include(i => i.Applicant).Include(i => i.Employee).Include(i => i.InterviewStatu).Include(i => i.Vacancy);
            return View(interviews.ToList());
        }

        // GET: Interviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewDTO data = new InterviewDTO();
            Interview interview = db.Interviews.Find(id);
            Applicant applicant = db.Applicants.Find(interview.ApplicantId);
            Vacancy vacant = db.Vacancies.Find(interview.VacancyId);
            data.Interview = interview;
            data.Vacancy = vacant;
            data.Applicant = applicant;
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult ScheduleInterview(InterviewDTO data)
        {
            if (DateTime.Compare((DateTime)data.Interview.InterviewDate, DateTime.Now) <= 0)
            {
                ViewData["Error"] = "Cannot Schedule an Interview in the Past";
                return Details(data.Interview.Id);
            }
            try
            {
                MailUtils.SendMail(data.Applicant.Email, data.Interview.Employee.FullName, data.Interview.Employee.Email, data.Vacancy.Name, data.Interview.InterviewDate, data.Interview.StartTime, data.Interview.EndTime);
                db.sp_ScheduleInterview(data.Interview.Id, data.Interview.InterviewDate, data.Interview.StartTime, data.Interview.EndTime, data.Interview.ApplicantId);
                TempData["Completed"] = "Successfully Scheduled an Interview";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.InnerException.Message;
                return Details(data.Interview.Id);
            }
        }

        public ActionResult AcceptInterview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            db.sp_AcceptCandidate(id, interview.ApplicantId, interview.VacancyId);
            TempData["Completed"] = "Successfully Accepted the Candidate";
            return RedirectToAction("Index");
        }
        public ActionResult RejectInterview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            db.sp_RejectCandidate(id, interview.ApplicantId);
            TempData["Completed"] = "Successfully Rejected the Candidate";
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
