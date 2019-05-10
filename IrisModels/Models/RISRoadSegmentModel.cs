using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Road Inventory", Display = "Road Segments", ListValue = "RISRoadSegment")]
    [Security(Module = "Road Inventory", Display = "Road Segments", ListValue = "RISRoadSegment")]
    public sealed class RISRoadSegmentModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int RISRoadSegment_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(RoadModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Road Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 Road_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "Begin Milepost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal BeginMilepost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "End Milepost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal EndMilepost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 4)]
		[IsExcludeSql]
		[DataType("Decimal")]
		[Range(0,99999)]
		[FilterType(Number = true)]
		[Display(Name = "Total Segment Miles")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalSegmentMiles { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISLookupFederalFunctionalClassModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Federal Functional Class Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupFederalFunctionalClass_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISCountyFunctionalClassModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS County Functional Class Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISCountyFunctionalClass_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISLookupSurfaceModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Surface Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupSurface_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "Surface Type Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SurfaceTypeWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISLookupJurisdictionModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Jurisdiction Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupJurisdiction_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISLookupCityModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup City Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupCity_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RISMaintenanceDistrictModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Maintenance District Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISMaintenanceDistrict_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "MPO")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte MPO { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Lanes")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Lanes { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Lane Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LaneWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[IsExcludeSql]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Lane Miles")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LaneMiles { get; set; }

    }
}
