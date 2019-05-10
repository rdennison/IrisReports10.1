using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/KeyTranslation/Read", "~/API/KeyTranslation/Create", "~/API/KeyTranslation/Update", "~/API/KeyTranslation/Destroy")]
    public sealed class KeyTranslationModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Char, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public string TableName { get; set; }

		[Key]
		[DbProperties(DatabaseType = SqlDbType.Char, Size = 10)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public string IRIS09Key { get; set; }

		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 IRIS10Key { get; set; }

    }
}
