using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    public sealed class UpdateRatesLogModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 UpdateRatesLog_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2500)]
		[FilterType(Text = true)]
		[Display(Name = "Update Sql")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string UpdateSql { get; set; }

    }
}
