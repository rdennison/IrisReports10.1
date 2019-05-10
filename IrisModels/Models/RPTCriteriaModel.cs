//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTCriteria/Read", "~/API/RPTCriteria/Create", "~/API/RPTCriteria/Update", "~/API/RPTCriteria/Destroy")]
    public sealed class RPTCriteriaModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string RPTCriteria_Key { get; set; }

        [Display(Name = "Security Object")]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type")]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        [MaxLength(1)]
        [Display(Name = "Key Field")]
        [DataType("Boolean")]
        public string KeyField { get; set; }

        [Required]
        [Display(Name = "Use Dropdown")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public string UseDropdown { get; set; }

        [Required]
        [MaxLength(1)]
        [Display(Name = "Use Non-Unique List")]
        [DataType("Boolean")]
        public string UseNonUniqueList { get; set; }

        [Required]
        [MaxLength(1)]
        [Display(Name = "Use Like Operator")]
        [DataType("Boolean")]
        public string UseLikeOperator { get; set; }

        [Display(Name = "Sproc Name")]
        [MaxLength(40)]
        public string SProcName { get; set; }

        [Required]
        [Display(Name = "Required")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public string Required { get; set; }

        [Required]
        [Display(Name = "Allow Sort")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public string AllowSort { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }

        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }
    }
}