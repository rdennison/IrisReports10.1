using IrisAttributes;
using Iris10ReportUI.GridBuilder.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [ModelDataBindings(DatabaseName = "IrisAuth", KeyFieldName = "TenantUser_Key", TableName = "TenantUser")]
    public sealed class TenantUserModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int TenantUser_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[ForeignKey(typeof(IRISUserModel), ForeignKeyDisplayField="NameDesc")]
		[DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "User Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public int? User_Key { get; set; }

    }
}
