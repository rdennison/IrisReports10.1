using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTPolk_PermitOpenDrainageCulvert/Read", "~/API/RPTPolk_PermitOpenDrainageCulvert/Create", "~/API/RPTPolk_PermitOpenDrainageCulvert/Update", "~/API/RPTPolk_PermitOpenDrainageCulvert/Destroy")]
    public sealed class RPTPolk_PermitOpenDrainageCulvertModel : ModelBase
    {
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Number = true)]
		[Display(Name = "Permit Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64 Permit_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[FilterType(Text = true)]
		[Display(Name = "Permit Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PermitNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Permit Type Of Access")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PermitTypeOfAccess { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Issue Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? IssueDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 44)]
		[FilterType(Text = true)]
		[Display(Name = "Owner Full Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OwnerFullName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Owner Address 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OwnerAddress1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 45)]
		[FilterType(Text = true)]
		[Display(Name = "Owner City State Zip")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OwnerCityStateZip { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Road Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string RoadName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 13)]
		[FilterType(Text = true)]
		[Display(Name = "Road Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string RoadNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Site Address 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SiteAddress1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Township")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Township { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Range")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Range { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Section")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Section { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Taxlot Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TaxlotNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 25)]
		[FilterType(Text = true)]
		[Display(Name = "Placement")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Placement { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Measured From")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string MeasuredFrom { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "Begin Milepost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? BeginMilepost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Culvert Diameter")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? CulvertDiameter { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Culvert Length")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? CulvertLength { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Surface Type")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SurfaceType { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Curb")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Curb { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Width { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Setback Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SetbackWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Sidewalk Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SidewalkWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Sidewalk Length")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SidewalkLength { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comment")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comment { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Issued By Position")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IssuedByPosition { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Top Of Access Distance From Traffic Lane")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? TopOfAccessDistanceFromTrafficLane { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Minimum Below Edge Of Traffic Lane")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MinimumBelowEdgeOfTrafficLane { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Drawing Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DrawingNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Slope Access Feet From Traffic Lane")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SlopeAccessFeetFromTrafficLane { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Slope Access Percent From Traffic Lane")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? SlopeAccessPercentFromTrafficLane { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Clearance Feet")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? ClearanceFeet { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Distance To Ditch")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? DistanceToDitch { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Slope")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Slope { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Radius")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Radius { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Angle")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Angle { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Driveway Width")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? DrivewayWidth { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Minimum Sight Distance Left")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MinimumSightDistanceLeft { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Minimum Sight Distance Right")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MinimumSightDistanceRight { get; set; }

		//[DbProperties(DatabaseType = SqlDbType.VarBinary, Size = -1)]
		//TODO:  Rexamine at a later date in order to determine a data type for varbinary
		//public varbinary? Signature { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string UserName { get; set; }

    }
}
