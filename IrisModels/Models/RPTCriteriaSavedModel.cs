//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTCriteriaSaved/Read", "~/API/RPTCriteriaSaved/Create", "~/API/RPTCriteriaSaved/Update", "~/API/RPTCriteriaSaved/Destroy")]
    public sealed class RPTCriteriaSavedModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string RPTCriteriaSaved_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Required]
        [Display(Name = "Save Name")]
        [MaxLength(30)]
        public string SaveName { get; set; }

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