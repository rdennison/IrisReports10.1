using IrisAttributes;
using SqlComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    class IrisApiModel : ModelBase
    {

        [Key]
        [IsAutoNumber]
        [ScaffoldColumn(false)]
        [IrisGridColumn(Hidden =true, IncludeInMenu =false)]
        public int IrisApi_Key { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [MaxLength(200)]
        [Display(Name = "API Name")]
        [IrisGridColumn(Width = 200)]       
        public string ApiName { get; set; }

        [Required(ErrorMessage ="Your {0} is required.")]
        [IrisGridColumn(DefaultValue = true)]
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [MaxLength(10)]
        public DateTime CreateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        public DateTime DateStamp { get; set; }
    }
}
