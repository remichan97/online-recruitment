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
	using System.Web.Mvc;

	public partial class Vacancy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacancy()
        {
            this.Interviews = new HashSet<Interview>();
        }
    
        public int Id { get; set; }
        [Required]
        [Display(Name = "Vacancy Title")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "Created On")]
        public System.DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Closing Date")]
        public Nullable<System.DateTime> ClosingDate { get; set; }
        [Required]
        [Display(Name = "Available Vacant")]
        public int Quantity { get; set; }
        public int EmployeeId { get; set; }
        public int Status { get; set; }
        [Display(Name = "Applied Vacant")]
        public int AppliedQuantity { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual VacancyStatu VacancyStatu { get; set; }
        public virtual Department Department { get; set; }
    }
}
