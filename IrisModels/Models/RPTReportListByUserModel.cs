using IrisAttributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RPTReportListByUser/Read", "~/API/RPTReportListByUser/Create", "~/API/RPTReportListByUser/Update", "~/API/RPTReportListByUser/Destroy")]
    public sealed class RPTReportListByUserModel : ModelBase
    {
        [DbProperties(DatabaseType = SqlDbType.Int)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Integer")]
        [Display(Name = "User Key")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public int User_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 101)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "User Name Desc")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public string UserNameDesc { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Integer")]
        [Display(Name = "Report Key")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public int Report_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [DataType("Boolean")]
        [Display(Name = "Favorite")]
        [IrisGridColumn(Width = 50)]
        public bool? Favorite { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Report")]
        [IrisGridColumn(Width = 200)]
        public string ReportName { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [DataType("Boolean")]
        [Display(Name = "Report Type")]
        [IrisGridColumn(Width = 50)]
        public bool? CustomReport { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 4000)]
        [Display(Name = "Report Description")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public string ReportDescription { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 4000)]
        [Display(Name = "Admin Comment")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public string AdminComment { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [Display(Name = "Designer Name")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public string DesignerName { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
        [DataType("Integer")]
        [Display(Name = "Report Access Key")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public int? ReportAccess_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [DataType("Boolean")]
        [Display(Name = "Read Only Access")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public bool? ReadOnlyAccess { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [DataType("Boolean")]
        [Display(Name = "Admin Access")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public bool? AdminAccess { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Int)]
        [DataType("Integer")]
        [Display(Name = "Report Favorite Key")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public int? ReportFavorite_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.NVarChar)]
        [DataType("String")]
        [Display(Name = "Tag List")]
        [IrisGridColumn(Width = 150, Hidden = true)]
        public string TagList { get; set; }
    }
}
