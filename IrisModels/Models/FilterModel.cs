//using IrisWeb.Code.Data.Attributes;
using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/Filter/Read", "~/API/Filter/Create", "~/API/Filter/Update", "~/API/Filter/Destroy")]
    [NoAutoSync]
    public sealed class FilterModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string Filter_Key { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string FilterType { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Description { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string TableName { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool KeyField { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool UseDropdown { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool UseNonUniqueList { get; set; }

        [MaxLength(40)]
        public string SProcName { get; set; }

        [MaxLength(40)]
        public string SPROC { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }
    }
}