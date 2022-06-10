using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_recruitment.Models
{
    public class InterviewDTO
    {
        public Interview Interview { get; set; }
        public Vacancy Vacancy { get; set; }
        public Applicant Applicant { get; set; }
    }
}