
using IrisAttributes;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    public sealed class RISLookupReflectivityConditionModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string RISLookupReflectivityCondition_Key { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Name - Description")]
        [IsExcludeSql]
        public string NameDesc { get; set; }

        [Display(Name = "Description - Name")]
        [IsExcludeSql]
        public string DescName { get; set; }



    
    }
}
