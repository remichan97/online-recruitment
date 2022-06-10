using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace online_recruitment.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please Enter your current Password")]
        [DisplayName("Current Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please Enter a new Password")]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please retype your new Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password do not match")]
        [DisplayName("Confirm your new Password")]
        public string ConfirmPassword { get; set; }
    }
}