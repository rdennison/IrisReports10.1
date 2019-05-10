using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Street Wise", Display = "Road Replacement Cost", ListValue = "SWSRoadReplacementCost")]
    [Security(Module = "Street Wise", Display = "Road Replacement Cost", ListValue = "SWSRoadReplacementCost")]
    public sealed class SWSRoadReplacementCostModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 SWSRoadReplacementCost_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(SWSLookupFederalFunctionalClassModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "SWS Lookup Federal Functional Class Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 SWSLookupFederalFunctionalClass_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(SWSLookupSurfaceModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "SWS Lookup Surface Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 SWSLookupSurface_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Number Of Lanes")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int NumberOfLanes { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Cost Perlinear Foot")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal CostPerlinearFoot { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Total Linear Feet")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalLinearFeet { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Total Cost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalCost { get; set; }

    }
}
