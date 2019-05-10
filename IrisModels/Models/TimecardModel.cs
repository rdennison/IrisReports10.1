using IrisAttributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Cost Accounting", Display = "Timecard", ListValue = "Timecard")]
    [Security(Module = "Cost Accounting", Display = "Timecard", ListValue = "Timecard")]
    [LookupDisplay(Name = "CAS - Timecard")]
    [EditorType(InCell = true)]
    public sealed class TimeCardModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        public Int64 TimeCard_Key { get; set; }

        [Display(Name = "Employee")]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [ForeignKey(typeof(EmployeeModel), ForeignKeyDisplayField = "FullNameEmployeeNum")]
        [FilterType(Dropdown = true)]
        [IrisGridColumn(Width = 250)]
        [CopyDown(true)]
        [Report(FkDescriptor = "LastName")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Employee_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Task Date")]
        [CopyDown(true)]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = false)]
        public DateTime TaskDate { get; set; }

        [Display(Name = "Fiscal Year")]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ForeignKey(typeof(FiscalModel), ForeignKeyDisplayField = "NameDesc")]
        [FilterType(Dropdown = true)]
        [CalculatedField(Init = true, RateType = "")]
        [Report(FkDescriptor = "NameDesc")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Fiscal_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 8)]
		[FilterType(Text = true)]
		[Display(Name = "Crew Num")]
		[IrisGridColumn(Width = 120)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string CrewNum { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Display(Name = "Activity")]
        [FilterType(Dropdown = true)]
        [IrisGridColumn(Width = 270)]
        [CopyDown(true)]
        [ForeignKey(typeof(ActivityModel), ForeignKeyDisplayField = "NameDesc")]
        [Report(FkDescriptor = "Description")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64 Activity_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Production")]
		[IrisGridColumn(Width = 130)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? Production { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Labor Hours")]
		[IrisGridColumn(Width = 120)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LaborQuantity { get; set; }

        [Display(Name = "Pay Type")]
        [IrisGridColumn(Width = 160)]
        [ForeignKey(typeof(PayTypeModel), ForeignKeyDisplayField = "NameDesc")]
        [CopyDown(true)]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [FilterType(Dropdown = true)]
        [Report(FkDescriptor = "NameDesc")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? PayType_Key { get; set; }

        [Display(Name = "Premium")]
        [ForeignKey(typeof(PremiumModel), ForeignKeyDisplayField = "NameDesc")]
        [FilterType(Dropdown = true)]
        [IrisGridColumn(Width = 120)]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Report(FkDescriptor = "NameDesc")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Premium_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Override Labor Rate")]
		[IrisGridColumn(Width = 190)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? OverrideLaborRate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Equipment Quantity")]
        [CalculatedField(Init = true, RateType = "")]
        [IrisGridColumn(Width = 140)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EquipmentQuantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(EquipmentModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Equipment")]
        [Report(FkDescriptor = "Description")]
        [IrisGridColumn(Width = 190)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Equipment_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Equipment Cost")]
		[IrisGridColumn(Width = 150)]
        [CantEdit]
        [CalculatedField(RateType = "EquipmentRate")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EquipmentUnitCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Equipment Odo Miles")]
		[IrisGridColumn(Width = 195)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EquipmentMiles { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 9, Scale = 2)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Equipment Odo Hours")]
		[IrisGridColumn(Width = 200)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EquipmentHours { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Material Quantity")]
        [CalculatedField(Init = true, RateType = "")]
        [IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MaterialQuantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(UOMModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Inventory UOM")]
        [IrisGridColumn(Width = 140)]
        [CantEdit]
        [CalculatedField(RateType = "InventoryLocationUOM")]
        [Report(FkDescriptor = "NameDesc")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? UOM_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Material Description")]
		[IrisGridColumn(Width = 207)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string MaterialDescription { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Material Unit Cost")]
		[IrisGridColumn(Width = 140)]
        [CalculatedField(RateType = "InventoryLocationCost")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? MaterialUnitCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(InventoryLocationModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Inventory Location")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 150)]
        [CopyDown(true)]
        [CalculatedField(Init = true, RateType = "")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? InventoryLocation_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Service Quantity")]
		[IrisGridColumn(Width = 135)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? OutSideServiceQuantity { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ResourceClassModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Resource Class")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 121, Hidden = true, IncludeInMenu = false)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? ResourceClass_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Service Description")]
		[IrisGridColumn(Width = 300)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OutSideServiceDescription { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
		[DataType("Currency")]
		[UIHint("Currency")]
		[Range(0,999999999999999)]
		[FilterType(Number = true)]
		[Display(Name = "Service Cost")]
		[IrisGridColumn(Width = 120)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? OutSideServiceCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(MgtUnitModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Mgt Unit")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 150)]
        [CopyDown(true)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? MgtUnit_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ProgramModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Program")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 120)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Program_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ZoneModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Zone")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Zone_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ProjectModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Project")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Project_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(ProjectSubModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Sub Project")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 200)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? ProjectSub_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(RBFModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Bridge/Facility")]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 250)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? RBF_Key { get; set; }

        [IrisGridColumn(Width = 234)]
        [ForeignKey(typeof(RoadModel), ForeignKeyDisplayField = "FullRoadNumber")]
        [FilterType(Dropdown = true)]
		[Display(Name = "Road")]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Report(FkDescriptor = "NameDesc")]
        [CalculatedField(Init = true, RateType = "")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Road_Key { get; set; }

        [Display(Name = "Road Name")]
        [IrisGridColumn(Width = 300)]
        [ForeignKey(typeof(RoadNameModel), ForeignKeyDisplayField = "NameDesc")]
        [FilterType(Dropdown = true)]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [CalculatedField(RateType = "RoadNumber")]
        [Report(FkDescriptor = "NameDesc")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? RoadName_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "Beg Point")]
		[IrisGridColumn(Width = 140)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? BegPoint { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
		[DataType("Decimal")]
		[Range(0,9999)]
		[FilterType(Number = true)]
		[Display(Name = "End Point")]
		[IrisGridColumn(Width = 140)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? EndPoint { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "From Location")]
		[IrisGridColumn(Width = 190)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string FromLocation { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "To Location")]
		[IrisGridColumn(Width = 190)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ToLocation { get; set; }
        
		[FilterType(Dropdown = true)]
		[Display(Name = "Reason")]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Report(FkDescriptor = "NameDesc")]
        [IrisGridColumn(Width = 150)]
        [ForeignKey(typeof(ReasonModel), ForeignKeyDisplayField = "Reason")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public Int64? Reason_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
		[FilterType(Text = true)]
		[Display(Name = "SRS Request Number")]
		[IrisGridColumn(Width = 140)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SRSRequestNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comments")]
		[IrisGridColumn(Width = 150)]
        [CopyDown(true)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comments { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 1")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 2")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User2 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 3")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User3 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 4")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User4 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 5")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User5 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 6")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User6 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 7")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User7 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 8")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User8 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 9")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User9 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "User 10")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [UserDefined]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string User10 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 255)]
		[FilterType(Text = true)]
		[Display(Name = "Error Message")]
		[IrisGridColumn(Width = 190)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ErrorMessage { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Posted")]
		[IrisGridColumn(Width = 150)]
        [CantEdit]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool Posted { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Short")]
		[Display(Name = "Fuel Import")]
		[IrisGridColumn(Width = 150)]
        [CantEdit]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte FuelImport { get; set; }

		[ForeignKey(typeof(DimDateModel))]
        [IsExcludeSql]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        [DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Dim Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? DimDate_Key { get; set; }

    }
}
