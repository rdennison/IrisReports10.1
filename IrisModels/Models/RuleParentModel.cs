//using IrisWeb.Code.Data.Attributes;
using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RuleParent/Read", "~/API/RuleParent/Create", "~/API/RuleParent/Update", "~/API/RuleParent/Destroy")]
    public sealed class RuleParentModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string RuleParent_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Required]
        [Display(Name = "Table Name")]
        [MaxLength(30)]
        public string TableName { get; set; }

        [Required]
        [Display(Name = "Rule Name")]
        [MaxLength(30)]
        public string RuleName { get; set; }

        [Required]
        [Display(Name = "Rule Description")]
        [MaxLength(1024)]
        public string RuleDescription { get; set; }

        [Required]
        [Display(Name = "Parent Where Clause")]
        [MaxLength(2000)]
        public string ParentWhereClause { get; set; }

        [Required]
        [Display(Name = "Child Where Clause")]
        [MaxLength(2000)]
        public string ChildWhereClause { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        [Display(Name = "Active?")]
        public string Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        //[ReadOnly(true)]
        [MaxLength(10)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }
    }
}