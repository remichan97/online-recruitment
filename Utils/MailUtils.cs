using System.Net;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_recruitment.Utils
{
    public class MailUtils
    {
        /// <summary>
        /// Send mail. Please change the sender and configuration in this method, as the given one are mocked-up info only for the sake of privacy
        /// </summary>
        /// <param name="applicantName">Name of the Applicant</param>
        /// <param name="vacancyName">Name of the desired job</param>
        /// <param name="interviewDate">Interview date scheduled</param>
        /// <param name="start">Time start</param>
        /// <param name="end">Time end</param>
        public static void SendMail(string receiverEmail,string interviewName, string interviewerEmail,string vacancyName, DateTime interviewDate, TimeSpan start, TimeSpan end) {
            var sender = new MailAddress("exellentcoaching@gmail.com", "ABC Company HR GROUP");
            var receiver = new MailAddress(receiverEmail);
            var password = "30ac05f4fa070f0c97adda0ba160c5d961e91dfdce0f2f45713a5caccdd53c00";
            var subject = "Interviewing Details";
            var body = "Good day!" + Environment.NewLine + "This Email is to invite you to the interview for joining our company. The information are as follow:" + Environment.NewLine + "Job Title: " + vacancyName + Environment.NewLine + "Interview date: " + interviewDate.ToShortDateString() + Environment.NewLine + "Start at: " + start.ToString() + Environment.NewLine + "End at: " + end.ToString() + Environment.NewLine + "Please reach out to the interviewer at " + interviewerEmail + "should you have any query";
            var smtp = new SmtpClient {
                  Port = 587,  
                    EnableSsl = true,  
                    DeliveryMethod = SmtpDeliveryMethod.Network,  
                    UseDefaultCredentials = false,  
                    Credentials = new NetworkCredential(sender.Address, password) 
            };
            using (var mess = new MailMessage(sender, receiver) {
                Subject = subject,
                Body = body
            }) {
                smtp.Send(mess);
            }
        }

		internal static void SendMail(string email1, string fullName, string email2, string name, DateTime? interviewDate, TimeSpan? startTime, TimeSpan? endTime)
		{
			throw new NotImplementedException();
		}
	}
}