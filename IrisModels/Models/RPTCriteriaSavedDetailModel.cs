//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTCriteriaSavedDetail/Read", "~/API/RPTCriteriaSavedDetail/Create", "~/API/RPTCriteriaSavedDetail/Update", "~/API/RPTCriteriaSavedDetail/Destroy")]
    public sealed class RPTCriteriaSavedDetailModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string RPTCriteriaSavedDetail_Key { get; set; }

        [Display(Name = "RPT Criteria Saved")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(RPTCriteriaSavedModel))]
        public string RPTCriteriaSaved_Key { get; set; }

        [Display(Name = "RPT Criteria")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(RPTCriteriaModel))]
        public string RPTCriteria_Key { get; set; }

        [Required]
        [Display(Name = "Value 1")]
        [MaxLength(100)]
        public string Value1 { get; set; }
        
        [Display(Name = "Value 2")]
        [MaxLength(100)]
        public string Value2 { get; set; }

        [Required]
        [Display(Name = "Operator 1")]
        [MaxLength(8)]
        public string Operator1 { get; set; }

        [Required]
        [Display(Name = "Operator 2")]
        [MaxLength(8)]
        public string Operator2 { get; set; }
        
        [Display(Name = "Group 1")]
        [MaxLength(10)]
        public string Group1 { get; set; }

        [Display(Name = "Group 2")]
        [MaxLength(10)]
        public string Group2 { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        [Display(Name = "Ordinal Position")]
        public bool OrdinalPosition { get; set; }

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