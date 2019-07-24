using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/ReportLog/Read", "~/API/ReportLog/Create", "~/API/ReportLog/Update", "~/API/ReportLog/Destroy")]
    public sealed class ReportLogModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		public int ReportLog_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(ReportModel), ForeignKeyDisplayField="NameDesc")]
		public int Report_Key { get; set; }
        
		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		public string Criteria { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2000)]
		public string ErrorMessage { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
        public int PageCount { get; set; }

    }
}
