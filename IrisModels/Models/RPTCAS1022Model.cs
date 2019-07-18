using Iris10ReportUI.GridBuilder.Attributes;
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
    [ModelDataBindings(DatabaseName = "User40", TableName = "RPTCAS1022", KeyFieldName = "Transact_Key ")]
    public sealed class RPTCAS1022Model : ModelBase
    {

        [Key]
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public Int64 Transact_Key { get; set; }

        
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public Int64 Road_Key { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 13)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [FilterType(Text = true)]
        [Display(Name = "Road Number")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string RoadNumber { get; set; }


        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public Int64 Activity_Key { get; set; }

       
        [DbProperties(DatabaseType = SqlDbType.BigInt)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public Int64 ResourceType_Key { get; set; }


        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
        [FilterType(Text = true)]
        [Display(Name = "Common Road")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string CommonRoad { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Decimal")]
        [Range(0, 9999)]
        [FilterType(Number = true)]
        [Display(Name = "Begin Milepost")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
        public decimal BeginMilepost { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 8, Scale = 4)]
        [DataType("Decimal")]
        [Range(0, 9999)]
        [FilterType(Number = true)]
        [Display(Name = "End Milepost")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
        public decimal? EndMilepost { get; set; }


        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 63)]
        [FilterType(Text = true)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string ActivityDesc { get; set; }


        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
        [FilterType(Text = true)]
        [Display(Name = "Description")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string Description { get; set; }


        [DbProperties(DatabaseType = SqlDbType.Money, Precision = 19, Scale = 4)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Range(0, 999999999999999)]
        [FilterType(Number = true)]
        [Display(Name = "Resource Rate")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
        public decimal? ResourceRate { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 13, Scale = 4)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Decimal")]
        [FilterType(Number = true)]
        [Display(Name = "Quantity")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
        public decimal Quantity { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Decimal, Precision = 33, Scale = 8)]
        [IsExcludeSql]
        [DataType("Decimal")]
        [FilterType(Number = true)]
        [Display(Name = "Total Resource Cost")]
        [Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
        public decimal? TotalResourceCost { get; set; }

        [DbProperties(DatabaseType = SqlDbType.Date)]
        [UIHint("IRISDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [FilterType(Date = true)]
        [Display(Name = "Task Date")]
        [IrisGridColumn(Width = 150)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public DateTime? TaskDate { get; set; }

        [DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
        [FilterType(Text = true)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Your {0} is required.")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        public string ResourceTypeDesc { get; set; }




    }
}
