using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/Months/Read", "~/API/Months/Create", "~/API/Months/Update", "~/API/Months/Destroy")]
    public sealed class MonthsModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Char, Size = 2)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public string Number { get; set; }

    }
}
