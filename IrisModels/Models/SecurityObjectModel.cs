using IrisAttributes;
using System;
//using IrisWeb.Code.Data.Attributes;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/SecurityObject/Read", "~/API/SecurityObject/Create", "~/API/SecurityObject/Update", "~/API/SecurityObject/Destroy")]
    //[ModelDataBindings(DatabaseName = "CountyDatabase")]
    [Report(NotReportable = true)]
    public sealed class SecurityObjectModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        //[HiddenInput]
        [ScaffoldColumn(false)]
        public string SecurityObject_Key { get; set; }

        //[OrderByFieldAttribute(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "Object Title")]
        public string ObjectTitle { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Module")]
        public string Module_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(15)]
        [Display(Name = "Object Type")]
        public string ObjectType { get; set; }

        [MaxLength(10)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Report Has Summary and Detail")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool SummaryDetail { get; set; }

        [MaxLength(10)]
        public string Report_Type_Key { get; set; }

        [MaxLength(100)]
        [Display(Name = "Base Table")]
        public string BaseTable { get; set; }

        [MaxLength(-1)]
        [Display(Name = "Special Notes")]
        public string SpecialNotes { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Used By All Counties")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool AllCounties { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Allow Optional Report Sorting")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowSort { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Enabled")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool Enabled { get; set; }

        [MaxLength(50)]
        [Display(Name = "Disabled Description")]
        public string DisabledDescription { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Allow Security")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowSecurity { get; set; }

        //TODO: Missing foreign key
        [Display(Name = "Child Security Key")]
        [MaxLength(10)]
        public string ChildSecurityObject_Key { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        //[ReadOnly(true)]
        [MaxLength(10)]
        public DateTime? CreateDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        [MaxLength(10)]
        public DateTime? DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }

    }

    
}