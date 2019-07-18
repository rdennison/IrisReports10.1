using IrisAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisModels.Models
{
    public sealed class RPTReportAvailableFilterList : ModelBase
    {
        [Key]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Width = 150)]
        public int ReportAvailableFilter_Key { get; set; }

        [ForeignKey(typeof(ReportModel))]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Width = 150)]
        public int Report_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Width = 150)]
        public string ColumnName { get; set; }


        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [IrisGridColumn(Width = 150)]
        public string FriendlyName { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Bit)]
        [IrisGridColumn(Width = 150)]
        public bool RequiredFilter { get; set; }


        [DbProperties(DatabaseType = SqlDbType.NVarChar)]
        [IrisGridColumn(Width = 150)]
        public string AllowedOperator { get; set; }

        [DbProperties(DatabaseType = SqlDbType.NVarChar)]
        [IrisGridColumn(Width = 150)]
        public string CustomList { get; set; }


        [DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 5)]
        [IrisGridColumn(Width = 150)]
        public string LookupList { get; set; }

    }
}
