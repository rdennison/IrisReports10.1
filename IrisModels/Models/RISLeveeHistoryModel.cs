using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [LookupDisplay(Name = "RIS - Levee History")]
    public sealed class RISLeveeHistoryModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 RISLeveeHistory_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(RISLeveeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Levee Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 RISLevee_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "History Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? HistoryDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(RISLookupLeveeHistoryActionModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee History Action Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 RISLookupLeveeHistoryAction_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		//[ForeignKey(typeof(RISLookupLeveeOverAllConditionUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Over All Condition UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 RISLookupLeveeOverAllConditionUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comment")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comment { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeOperationManualUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Operation Manual UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeOperationManualUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeEmergencySuppliesUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Emergency Supplies UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeEmergencySuppliesUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeFloodPreparednessUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Flood Preparedness UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeFloodPreparednessUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeVegetationGrowthUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Vegetation Growth UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeVegetationGrowthUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeEncroachmentsUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Encroachments UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeEncroachmentsUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeSlopeStabilityUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Slope Stability UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeSlopeStabilityUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeErosionUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Erosion UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeErosionUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeSettlementUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Settlement UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeSettlementUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeDepressionRuttingUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Depression Rutting UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeDepressionRuttingUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeCrackingUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Cracking UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeCrackingUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeAnimalControlUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Animal Control UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeAnimalControlUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeRiprapRevetmentsBankProtectionUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Riprap Revetments Bank Protection UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeRiprapRevetmentsBankProtectionUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeRevetmentsOtherThanRiprapUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Revetments Other Than Riprap UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeRevetmentsOtherThanRiprapUVW_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		//[ForeignKey(typeof(RISLookupLeveeSeepageUVWModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "RIS Lookup Levee Seepage UVW Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? RISLookupLeveeSeepageUVW_Key { get; set; }

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

    }
}
