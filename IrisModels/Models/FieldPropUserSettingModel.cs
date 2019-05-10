//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FieldPropUserSetting/Read", "~/API/FieldPropUserSetting/Create", "~/API/FieldPropUserSetting/Update", "~/API/FieldPropUserSetting/Destroy")]
    public sealed class FieldPropUserSettingModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string FieldProp_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsEnabled { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsRequired { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
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