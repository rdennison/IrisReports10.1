using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class ReportModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int Report_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ReportName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 4000)]
		[FilterType(Text = true)]
		[Display(Name = "Description")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Description { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 4000)]
        [FilterType(Text = true)]
        [Display(Name = "Admin Name")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string AdminComment{ get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [FilterType(Text = true)]
        [Display(Name = "Name")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string DesignerName { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [FilterType(Text = true)]
        [Display(Name = "Name")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public byte CustomReport { get; set; }






    }
}
