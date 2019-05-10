//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FilterRPTSort/Read", "~/API/FilterRPTSort/Create", "~/API/FilterRPTSort/Update", "~/API/FilterRPTSort/Destroy")]
    public sealed class FilterRPTSortModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string FilterRPTSort_Key { get; set; }

        [Display(Name = "Filter Sys Data")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(FilterSysDataModel))]
        public string FilterSysData_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [MaxLength(30)]
        [Required]
        public string SavedName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool OrdinalPosition { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool SortAscending { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }
    }
}