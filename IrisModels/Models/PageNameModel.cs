using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/PageName/Read", "~/API/PageName/Create", "~/API/PageName/Update", "~/API/PageName/Destroy")]
    public sealed class PageNameModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int PageName_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Page Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PageName { get; set; }

    }
}
