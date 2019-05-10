using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace IrisModels.Models
{
    //[IrisGrid("~/API/SecurityUser/Read", "~/API/SecurityUser/Create", "~/API/SecurityUser/Update", "~/API/SecurityUser/Destroy")]
    [Report(NotReportable = true)]
    public sealed class SecurityUserModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string SecurityUser_Key { get; set; }

        //[OrderByFieldAttribute(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "Login Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Employee")]
        [ForeignKey(typeof(EmployeeModel))]
        [MaxLength(10)]
        public string Employee_Key { get; set; }

        [Display(Name = "Password")]
        [MaxLength(50)]
        public string NetPassword { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [MaxLength(10)]
        //[ReadOnly(true)]
        public DateTime? CreateDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        [MaxLength(10)]
        public DateTime? DateStamp { get; set; }

        [Display(Name = "Entered By")]
        [MaxLength(50)]
        public string Entered_By { get; set; }
    }
}
