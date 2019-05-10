using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class CRWSaveReportUserModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string CRWSaveReportUser_Key { get; set; }

        [Display(Name = "CRW Save Report")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(CRWSaveReportModel))]
        public string CRWSaveReport_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool SecurityLevel { get; set; }

        [DataType("Integer")]
        [MaxLength(4)]
        public int TotalRan { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Ran")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? LastRanDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? CreateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        public string SecurityUser_Key { get; set; }
    }
}