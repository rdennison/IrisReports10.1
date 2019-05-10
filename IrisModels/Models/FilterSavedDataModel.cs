//using IrisWeb.Code.Data.Attributes;
using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FilterSavedData/Read", "~/API/FilterSavedData/Create", "~/API/FilterSavedData/Update", "~/API/FilterSavedData/Destroy")]
    public sealed class FilterSavedDataModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string FilterSavedData_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Display(Name = "Filter Sys Data")]
        [MaxLength(10)]
        //[ForeignKey(typeof(FilterSysDataModel))]
        public string FilterSysData_Key { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string SavedName { get; set; }

        [MaxLength(100)]
        public string Value1 { get; set; }

        [MaxLength(100)]
        public string Value2 { get; set; }

        [MaxLength(16)]
        [Required]
        public string Operator1 { get; set; }

        [MaxLength(8)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Operator2 { get; set; }

        [MaxLength(10)]
        public string Group1 { get; set; }

        [MaxLength(10)]
        public string Group2 { get; set; }

        [DataType("Integer")]
        [MaxLength(4)]
        public int RecordOrder { get; set; }

        [MaxLength(4096)]
        public string EnglishWhere { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        public string ForeignKey { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsRequired { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [MaxLength(10)]
        //[ReadOnly(true)]
        public DateTime? CreateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }
    }
}