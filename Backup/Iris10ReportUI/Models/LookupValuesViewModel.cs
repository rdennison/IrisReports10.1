using IrisAttributes;
using IrisModels.Models;
using System.ComponentModel.DataAnnotations;

namespace Iris10ReportUI.Models
{
    [EditorType(InCell = true)]
    [BatchMode] //turn off filter and grid config
    public class LookupValuesViewModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [ScaffoldColumn(false)]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        public string Model_Key { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //TODO: change pivot logic at some point probably
        [Display(Name = "Name - Description")]
        [CantEdit]
        public string NameDesc { get; set; }

        //TODO: change pivot logic at some point probably
        [Display(Name = "Description - Name")]
        [CantEdit]
        public string DescName { get; set; }
        
        [Display(Name = "Active")]
        [CantEdit]
        public bool Active { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "System Data")]
        [CantEdit]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        public bool IsSystemData { get; set; }
        
        [Display(Name = "County")]
        [CantEdit]
        [ForeignKey(typeof(CountyModel))]
        public string County_Key { get; set; }
        
    }
}