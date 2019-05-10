using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class AppCounterModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public string FieldName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Count Value")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal CountValue { get; set; }

    }
}
