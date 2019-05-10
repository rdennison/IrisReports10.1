﻿using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    public sealed class UseLocationModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 UseLocation_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Number = true)]
		[Display(Name = "Foreign Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64 Foreign_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Use Location Type")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte UseLocationType { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 75)]
		[FilterType(Text = true)]
		[Display(Name = "Nick Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string NickName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Is Private")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte IsPrivate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 40)]
		[FilterType(Text = true)]
		[Display(Name = "Address Line 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string AddressLine1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 40)]
		[FilterType(Text = true)]
		[Display(Name = "Address Line 2")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string AddressLine2 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 30)]
		[FilterType(Text = true)]
		[Display(Name = "City")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string City { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 30)]
		[FilterType(Text = true)]
		[Display(Name = "State")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string State { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 5)]
		[FilterType(Text = true)]
		[Display(Name = "Zip Code")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ZipCode { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 4)]
		[FilterType(Text = true)]
		[Display(Name = "Zip Code 4")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ZipCode4 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Zip Only")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte ZipOnly { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 75)]
		[FilterType(Text = true)]
		[Display(Name = "GPS Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string GPSName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Township Start")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TownshipStart { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Range Start")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string RangeStart { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2)]
		[FilterType(Text = true)]
		[Display(Name = "Section Start")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SectionStart { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Township End")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TownshipEnd { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 7)]
		[FilterType(Text = true)]
		[Display(Name = "Range End")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string RangeEnd { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2)]
		[FilterType(Text = true)]
		[Display(Name = "Section End")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SectionEnd { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 75)]
		[FilterType(Text = true)]
		[Display(Name = "TRS Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TRSName { get; set; }

    }
}
