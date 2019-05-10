using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Road Inventory", Display = "Average Daily Traffic", ListValue = "RISADT")]
    [Security(Module = "Road Inventory", Display = "Average Daily Traffic", ListValue = "RISADT")]
    public sealed class RISADTModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 RISADT_Key { get; set; }

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

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Begin Description")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string BeginDescription { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "End Description")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EndDescription { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Last Reading Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? LastReadingDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "Milepost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Milepost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Unadjusted ADT")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? UnadjustedADT { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Adjusted ADT")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AdjustedADT { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Percent Trucks")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? PercentTrucks { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Days Counted")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? DaysCounted { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Counter ID Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string CounterIDNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comments")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comments { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 1")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 2")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User2 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 3")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User3 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 4")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User4 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 5")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User5 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 6")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User6 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 7")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User7 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 8")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User8 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 9")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User9 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 10")]
		[IrisGridColumn(Width = 150)]
		[UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User10 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 1)]
		[FilterType(Text = true)]
		[Display(Name = "Flag")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Flag { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Active")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte Active { get; set; }

    }
}
