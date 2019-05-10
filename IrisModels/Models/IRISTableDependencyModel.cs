using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/IRISTableDependency/Read", "~/API/IRISTableDependency/Create", "~/API/IRISTableDependency/Update", "~/API/IRISTableDependency/Destroy")]
    public sealed class IRISTableDependencyModel : ModelBase
    {
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Table Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int Table_Key { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "Table Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TableName { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "IRIS 09 Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS09Name { get; set; }

		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Dependency Level")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? DependencyLevel { get; set; }

		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Has SYS Keys")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? HasSYSKeys { get; set; }

		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Translate SYS Keys To Common Keys")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? TranslateSYSKeysToCommonKeys { get; set; }

		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Sync Status")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? SyncStatus { get; set; }

		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "CRUD Procedures")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? CRUDProcedures { get; set; }

		[FilterType(Text = true)]
		[Display(Name = "SYS Key Resolve Method")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SYSKeyResolveMethod { get; set; }

    }
}
