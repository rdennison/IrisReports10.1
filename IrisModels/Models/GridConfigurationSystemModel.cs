using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class GridConfigurationSystemModel : ModelBase
    {
		[Key]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 GridConfigurationSystem_Key { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Page Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PageName { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Configuration")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Configuration { get; set; }

    }
}
