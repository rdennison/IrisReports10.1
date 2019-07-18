using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [EditorType(InCell = true)]
    [Report(NotReportable = true)]
    public sealed class GridFilterWhereModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150, Hidden = true)]
		public int GridFilterWhere_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(GridFilterModel), ForeignKeyDisplayField="NameDesc")]
		[DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Grid Filter Key")]
		[IrisGridColumn(Width = 150, Hidden = true)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public int GridFilter_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Position")]
		[IrisGridColumn(Width = 150, Hidden = true)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int Position { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[FilterType(Text = true)]
		[Display(Name = "Open Group")]
        [FilterDropdown(Group1 = true)]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OpenGroup { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 128)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Dropdown = true)]
        [FilterDropdown(Description = true)]
		[Display(Name = "Column Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ColumnName { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [FilterType(Text = true)]
        [FilterDropdown(Operator = true)]
        [Display(Name = "Comparison Operator")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string ComparisonOperator { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
        [FilterDropdown(Value1 = true)]
        [Display(Name = "Value 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Value1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "Value 2")]
        [FilterDropdown(Value2 = true)]
        [IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Value2 { get; set; }

	

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[FilterType(Text = true)]
		[Display(Name = "Close Group")]
		[IrisGridColumn(Width = 150)]
        [FilterDropdown(Group2 = true)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string CloseGroup { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 5)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "And Or")]
		[IrisGridColumn(Width = 150)]
        [FilterDropdown(Operator2 = true)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string AndOr { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "In List")]
		[IrisGridColumn(Width = 150, Hidden = true)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string InList { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 128)]
		[FilterType(Text = true)]
		[Display(Name = "Table Name")]
		[IrisGridColumn(Width = 150, Hidden = true)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TableName { get; set; }

        [IsExcludeSql]
        [DisableSqlReadAttribute]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public DropdownValuesViewModel DropdownValues { get; set; }

        [IsExcludeSql]
        [DisableSqlReadAttribute]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public DropdownValuesViewModel DropdownValues2 { get; set; }

        [IsExcludeSql]
        [DisableSqlReadAttribute]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public DropdownValuesViewModel DropdownValues3 { get; set; }

    }
}
