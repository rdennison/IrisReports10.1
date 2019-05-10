using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/ReportExample/Read", "~/API/ReportExample/Create", "~/API/ReportExample/Update", "~/API/ReportExample/Destroy")]
    public sealed class ReportExampleModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int ReportExample_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(ReportModel), ForeignKeyDisplayField="NameDesc")]
		[DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Report Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public int Report_Key { get; set; }

		//[DbProperties(DatabaseType = SqlDbType.VarBinary, Size = -1)]
		//TODO:  Rexamine at a later date in order to determine a data type for varbinary
		//public varbinary? ExampleReport { get; set; }

    }
}
