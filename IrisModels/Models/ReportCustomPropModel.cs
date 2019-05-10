using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/ReportCustomProp/Read", "~/API/ReportCustomProp/Create", "~/API/ReportCustomProp/Update", "~/API/ReportCustomProp/Destroy")]
    public sealed class ReportCustomPropModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int ReportCustomProp_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(ReportModel), ForeignKeyDisplayField="NameDesc")]
		[DataType("Integer")]
		[FilterType(Dropdown = true)]
		[Display(Name = "Report Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public int Report_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Base Model")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string BaseModel { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Models")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Models { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "Relationships")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Relationships { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "Select Props")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SelectProps { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "Filters")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Filters { get; set; }

    }
}
