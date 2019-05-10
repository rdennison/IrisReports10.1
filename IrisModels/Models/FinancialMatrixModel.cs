using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FinancialMatrix/Read", "~/API/FinancialMatrix/Create", "~/API/FinancialMatrix/Update", "~/API/FinancialMatrix/Destroy")]
    public sealed class FinancialMatrixModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 FinancialMatrix_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Number = true)]
		[Display(Name = "Pay Type Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64 PayType_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 12)]
		[FilterType(Text = true)]
		[Display(Name = "Supervisor Code")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SupervisorCode { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 12)]
		[FilterType(Text = true)]
		[Display(Name = "Union Code")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string UnionCode { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2)]
		[FilterType(Text = true)]
		[Display(Name = "Leave Code")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string LeaveCode { get; set; }

    }
}
