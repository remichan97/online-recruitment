using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace online_recruitment.Models
{
    public class UpdateInfo
    {
        [Required(ErrorMessage = "Please Enter your Full name")]
        [DisplayName("Full Name")]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required (ErrorMessage = "Please Enter your Email Address")]
        [EmailAddress]
        [StringLength(25)]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter your Phone number")]
        [Phone]
        [StringLength(50)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}