﻿using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/RISLookupApproachSurface/Read", "~/API/RISLookupApproachSurface/Create", "~/API/RISLookupApproachSurface/Update", "~/API/RISLookupApproachSurface/Destroy")]
    public sealed class RISLookupApproachSurfaceModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 RISLookupApproachSurface_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Name { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 60)]
		[FilterType(Text = true)]
		[Display(Name = "Description")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Description { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 73)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IsExcludeSql]
		[FilterType(Text = true)]
		[Display(Name = "Name Desc")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string NameDesc { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 73)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IsExcludeSql]
		[FilterType(Text = true)]
		[Display(Name = "Desc Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DescName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Is System Data")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool IsSystemData { get; set; }

    }
}
