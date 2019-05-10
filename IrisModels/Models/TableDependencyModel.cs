using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    public sealed class TableDependencyModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int TableDependency_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Table Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TableName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "IRIS 09 Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS09Name { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Dependency Level")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? DependencyLevel { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Has SYS Keys")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? HasSYSKeys { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Translate SYS Keys To Common Keys")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? TranslateSYSKeysToCommonKeys { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Sync Status")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? SyncStatus { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "CRUD Procedures")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? CRUDProcedures { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "SYS Key Resolve Method")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SYSKeyResolveMethod { get; set; }

    }
}
