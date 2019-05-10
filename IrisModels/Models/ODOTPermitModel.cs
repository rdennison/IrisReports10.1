using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "SERVICES", Display = "ODOT Permit", ListValue = "ODOTPermit")]
    [LookupDisplay(Name = "SERVICES - ODOT Permit")]
    public sealed class ODOTPermitModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 ODOTPermit_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Permit Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? PermitDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Effective Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? EffectiveDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Expiration Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? ExpirationDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ODOTLookupIssuerModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "ODOT Lookup Issuer Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? ODOTLookupIssuer_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "ODOT Permit Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int ODOTPermitNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ODOTLookupCarrierModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "ODOT Lookup Carrier Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? ODOTLookupCarrier_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ODOTLookupCommodityModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "ODOT Lookup Commodity Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? ODOTLookupCommodity_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Load Length")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LoadLength { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Load Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LoadWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Load Height")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LoadHeight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Overall Length")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? OverallLength { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Total Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? TotalWeight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Number Of Axles")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? NumberOfAxles { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Front Overhang")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? FrontOverhang { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Rear Overhang")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? RearOverhang { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Weight Table")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? WeightTable { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Starting Point")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string StartingPoint { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Destination")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Destination { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Starting Address")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string StartingAddress { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Ending Address")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EndingAddress { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 500)]
		[FilterType(Text = true)]
		[Display(Name = "Route")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Route { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 500)]
		[FilterType(Text = true)]
		[Display(Name = "Other Route Condition")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OtherRouteCondition { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[DataType("Byte")]
		[Display(Name = "Weight And Spacing Needed")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte? WeightAndSpacingNeeded { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 1 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup1Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 2 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup2Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 3 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup3Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 4 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup4Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 5 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup5Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 6 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup6Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 7 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup7Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 8 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup8Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 9 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup9Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 10 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup10Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 11 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup11Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Group 12 Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleGroup12Weight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 2")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing2 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 3")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing3 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 4")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing4 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 5")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing5 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 6")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing6 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 7")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing7 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 8")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing8 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 9")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing9 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 10")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing10 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 11")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing11 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 18, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Axle Spacing 12")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? AxleSpacing12 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Overweight Check By")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OverweightCheckBy { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 3)]
		[FilterType(Text = true)]
		[Display(Name = "Director Approval Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DirectorApproval_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[FilterType(Number = true)]
		[Display(Name = "Security User 2 Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64? SecurityUser2_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Number Pilot Cars Front")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? NumberPilotCarsFront { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Number Pilot Cars Rear")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? NumberPilotCarsRear { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Number Of Pilot Car Required Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? NumberOfPilotCarRequired_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Total Trips")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? TotalTrips { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Route Height Pole")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte RouteHeightPole { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Legal Weight")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte LegalWeight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Legal Height")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte LegalHeight { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Legal Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte LegalWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Legal Length")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte LegalLength { get; set; }

    }
}
