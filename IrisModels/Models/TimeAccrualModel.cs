using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Security(Module = "Cost Accounting", Display = "Time Accrual", ListValue = "TimeAccrual")]
    public sealed class TimeAccrualModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 TimeAccrual_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(ActivityModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Activity Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 Activity_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(EmployeeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Employee Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 Employee_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(PayTypeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Pay Type Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 PayType_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(PayTypeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Pay Type 2 Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 PayType2_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Hours")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal Hours { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Max Hours")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MaxHours { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(IntervalModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Interval Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 Interval_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[IsExcludeSql]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Total Accumulated Hours")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalAccumulatedHours { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[IsExcludeSql]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Total Consumed Hours")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalConsumedHours { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 14, Scale = 4)]
		[IsExcludeSql]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Current Balance")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? CurrentBalance { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[IsExcludeSql]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Last Accrual Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? LastAccrualDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comments")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comments { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Active")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte Active { get; set; }

    }
}
