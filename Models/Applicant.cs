//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace online_recruitment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

	public partial class Applicant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Applicant()
        {
            this.Interviews = new HashSet<Interview>();
        }
    
        public int Id { get; set; }
        [Required]
        [Display(Name = "Applicant Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public int Status { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string CvFile { get; set; }
        [Display(Name = "Applicant CV File")]

        public HttpPostedFileBase PdfFile {get;set;}
    
        public virtual ApplicantStatu ApplicantStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual Department Department { get; set; }
    }
}
