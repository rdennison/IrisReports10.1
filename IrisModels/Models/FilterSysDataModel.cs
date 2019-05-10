//using IrisWeb.Code.Data.Attributes;
using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FilterSysData/Read", "~/API/FilterSysData/Create", "~/API/FilterSysData/Update", "~/API/FilterSysData/Destroy")]
    public sealed class FilterSysDataModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string FilterSysData_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string OriginalDescription { get; set; }

        [MaxLength(10)]
        [Required]
        public string Type { get; set; }

        [DataType("Boolean")]
        [MaxLength(1)]
        public bool KeyField { get; set; }

        [DataType("Boolean")]
        [MaxLength(1)]
        public bool UseDropDown { get; set; }

        [DataType("Boolean")]
        [MaxLength(1)]
        public bool UseLikeOperator { get; set; }

        [MaxLength(50)]
        public string Sproc { get; set; }

        [MaxLength(512)]
        public string JoinSQL { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsRequired { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowSort { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool NoFilter { get; set; }

        [MaxLength(100)]
        public string DefaultFilter { get; set; }

        [DataType("Integer")]
        [MaxLength(4)]
        public int DefaultFilterOrder { get; set; }

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

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }
    }
}