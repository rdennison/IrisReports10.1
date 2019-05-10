//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTSortSaved/Read", "~/API/RPTSortSaved/Create", "~/API/RPTSortSaved/Update", "~/API/RPTSortSaved/Destroy")]
    public sealed class RPTSortSavedModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string RPTSortSaved_Key { get; set; }

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
        [DataType("Boolean")]
        [MaxLength(1)]
        [Display(Name = "Ordinal Position")]
        public bool OrdinalPosition { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        [Display(Name = "Sort Ascending")]
        public bool SortAscending { get; set; }

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