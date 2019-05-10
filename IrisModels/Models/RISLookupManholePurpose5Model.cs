using IrisAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisModels.Models
{
    public sealed class RISLookupManholePurpose5Model
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string RISLookupManholePurpose5_Key { get; set; }

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
