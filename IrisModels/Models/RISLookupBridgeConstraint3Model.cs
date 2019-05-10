using IrisAttributes;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    public sealed class RISLookupBridgeConstraint3Model
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string RISLookupBridgeConstraint3_Key { get; set; }

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

        [Display(Name = "System Data")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public bool IsSystemData { get; set; }

        [ForeignKey(typeof(CountyModel))]
        [Display(Name = "County")]
        public string County_Key { get; set; }
    }
}
