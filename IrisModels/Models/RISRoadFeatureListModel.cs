using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Road Inventory", Display = "Road Feature", ListValue = "RISRoadFeatureList")]
    [Security(Module = "Road Inventory", Display = "Road Feature", ListValue = "RISRoadFeatureList")]
    public sealed class RISRoadFeatureListModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int RISRoadFeatureList_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Feature")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Feature { get; set; }

    }
}
