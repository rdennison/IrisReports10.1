﻿using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [LookupDisplay(Name = "CAS - Employee Rates")]
    [EditorType(InCell = true)]
    [IrisGridColumn(ScreenName = "EmployeeRates")]
    public sealed class EmployeeRateModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 EmployeeRate_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(EmployeeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Employee Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 Employee_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(PayChangeTypeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Pay Change Type Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? PayChangeType_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Beg Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? BegDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "End Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? EndDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Rate")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal Rate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Fringe Rate")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal FringeRate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Overhead Rate")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal OverheadRate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Other Rate")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal OtherRate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Full Burden Rate")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal FullBurdenRate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 32, Scale = 16)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "OH Cost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal OHCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "OH Calc Method")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte OHCalcMethod { get; set; }

    }
}
