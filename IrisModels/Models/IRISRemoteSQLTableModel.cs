using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/IRISRemoteSQLTable/Read", "~/API/IRISRemoteSQLTable/Create", "~/API/IRISRemoteSQLTable/Update", "~/API/IRISRemoteSQLTable/Destroy")]
    public sealed class IRISRemoteSQLTableModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int IRISRemoteSQLTable_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "SQL String")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SQLString { get; set; }

    }
}
