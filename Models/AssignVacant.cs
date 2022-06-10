using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_recruitment.Models
{
    public class AssignVacant
    {
        public Applicant applicant { get; set; }

        public List<VacancyDTO> vacancies { get; set; }
    }
}