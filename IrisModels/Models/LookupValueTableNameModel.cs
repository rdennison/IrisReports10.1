using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/LookupValueTableName/Read", "~/API/LookupValueTableName/Create", "~/API/LookupValueTableName/Update", "~/API/LookupValueTableName/Destroy")]
    public sealed class LookupValueTableNameModel : ModelBase
    {
		[Key]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 LookupValueTableName_Key { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Table Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TableName { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Description")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Description { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Is Active")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool IsActive { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Is System Data")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool IsSystemData { get; set; }

    }
}
