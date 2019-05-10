//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/LookupValuesDetail/Read", "~/API/LookupValuesDetail/Create", "~/API/LookupValuesDetail/Update", "~/API/LookupValuesDetail/Destroy")]
    public sealed class LookupValuesDetailModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string LookupValuesDetail_Key { get; set; }

        [Display(Name = "Lookup Values")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(LookupValuesModel))]
    
        public string LookupValues_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

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