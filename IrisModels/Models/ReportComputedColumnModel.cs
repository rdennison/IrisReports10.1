﻿using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/ReportParameter/Read", "~/API/ReportParameter/Create", "~/API/ReportParameter/Update", "~/API/ReportParameter/Destroy")]
    public sealed class ReportComputedColumnModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		public int ReportComputedColumn_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(ReportModel), ForeignKeyDisplayField="NameDesc")]
		public int Report_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		public string ColumnName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = -1)]
		public string ColumnValue { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2000)]
        public string ColumnDescription { get; set; }

    }
}