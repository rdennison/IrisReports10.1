using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    public sealed class UserSortJoinModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 USysUserSortJoin_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "User Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? User_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Number = true)]
		[Display(Name = "Sort Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64 Sort_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Ordinal Position")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? OrdinalPosition { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Ascending Order")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool AscendingOrder { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Is Selected")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool IsSelected { get; set; }

    }
}
