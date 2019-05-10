//using IrisWeb.Code.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/CountyUtility/Read", "~/API/CountyUtility/Create", "~/API/CountyUtility/Update", "~/API/CountyUtility/Destroy")]
    public sealed class CountyUtilityModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string CountyUtility_Key { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Menu")]
        public string MenuText { get; set; }

        [MaxLength(1)]
        [Required]
        [Display(Name = "Menu Index")]
        public string MenuIndex { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Function")]
        public string FunctionName { get; set; }

        [Display(Name = "County")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(CountyModel))]
        public string County_Key { get; set; }

        [Display(Name = "Security")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsIRISRemote { get; set; }
    }
}