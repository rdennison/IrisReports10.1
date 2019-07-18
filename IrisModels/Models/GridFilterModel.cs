using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class GridFilterModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
        [NoTenant]
        [Hidden]
		public int GridFilter_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
        [ForeignKey(typeof(ReportModel), ForeignKeyDisplayField = "ReportName")]
        [DataType("Integer")]
        [IrisGridColumn(Width = 150)]
        [Hidden]
        public int? Report_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(PageNameModel), ForeignKeyDisplayField="PageName")]
		[DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Page Name Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public int PageName_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Name { get; set; }

    }
}
