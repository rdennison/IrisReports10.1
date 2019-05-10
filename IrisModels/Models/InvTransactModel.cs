using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Cost Accounting", Display = "Inventory Transaction", ListValue = "InvTransact")]
    [Security(Module = "Cost Accounting", Display = "Inventory Transaction", ListValue = "InvTransact")]
    public sealed class InvTransactModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int InvTransact_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(InventoryLocationModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Inventory Location Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? InventoryLocation_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Transaction Type")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TransactionType { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Inv Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? InvDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(VendorModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Vendor Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? Vendor_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Tracking Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TrackingNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 25, Scale = 16)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Prev Quantity")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? PrevQuantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 32, Scale = 16)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Prev Price Value")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? PrevPriceValue { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 25, Scale = 16)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Quantity")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal Quantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 32, Scale = 16)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Price Value")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal PriceValue { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 25, Scale = 16)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Adjusted Quantity")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AdjustedQuantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 32, Scale = 16)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Adjusted Price Value")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AdjustedPriceValue { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 32, Scale = 16)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Total Cost Change")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TotalCostChange { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Labor Cost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LaborCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Equipment Cost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EquipmentCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 512)]
		[FilterType(Text = true)]
		[Display(Name = "Comments")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comments { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[FilterType(Number = true)]
		[Display(Name = "Transact Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64? Transact_Key { get; set; }

    }
}
