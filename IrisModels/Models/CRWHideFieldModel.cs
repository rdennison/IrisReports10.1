//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/CRWHideField/Read", "~/API/CRWHideField/Create", "~/API/CRWHideField/Update", "~/API/CRWHideField/Destroy")]
    public sealed class CRWHideFieldModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        //[IsAutoNumber]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(4)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string CRWHideField_Key { get; set; }

        [Display(Name = "Field Prop")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(FieldPropModel))]
        public string FieldProp_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        public string SecurityUser_Key { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? CreateDate { get; set; }
    }
}