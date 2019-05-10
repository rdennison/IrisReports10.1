using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/IRISTableColumnRename/Read", "~/API/IRISTableColumnRename/Create", "~/API/IRISTableColumnRename/Update", "~/API/IRISTableColumnRename/Destroy")]
    public sealed class IRISTableColumnRenameModel : ModelBase
    {
		[Key]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int IRISTableColumnRename_Key { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "IRIS 10 Table Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS10TableName { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "IRIS 10 Column Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS10ColumnName { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "IRIS 09 Table Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS09TableName { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "IRIS 09 Column Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS09ColumnName { get; set; }

    }
}
