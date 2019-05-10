using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/LookupValueTableNameDetail/Read", "~/API/LookupValueTableNameDetail/Create", "~/API/LookupValueTableNameDetail/Update", "~/API/LookupValueTableNameDetail/Destroy")]
    public sealed class LookupValueTableNameDetailModel : ModelBase
    {
		[Key]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 LookupValueTableNameDetail_Key { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(LookupValueTableNameModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Lookup Value Table Name Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 LookupValueTableName_Key { get; set; }

		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(PageNameModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Page Name Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 PageName_Key { get; set; }

    }
}
