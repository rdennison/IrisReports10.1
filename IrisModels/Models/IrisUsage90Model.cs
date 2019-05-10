//using IrisWeb.Code.Data.Attributes;
using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/IrisUsage90/Read", "~/API/IrisUsage90/Create", "~/API/IrisUsage90/Update", "~/API/IrisUsage90/Destroy")]
    public sealed class IrisUsage90Model
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        //[IsAutoNumber]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(4)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string IrisUsage90_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        public string ObjectTitle { get; set; }

        [Display(Name = "County")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(CountyModel))]
        public string County_Key { get; set; }

        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityUserModel))]
        public string SecurityUser_Key { get; set; }

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