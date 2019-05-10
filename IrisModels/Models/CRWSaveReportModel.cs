using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class CRWSaveReportModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string CRWSaveReport_Key { get; set; }

        [Display(Name = "Module")]
        [MaxLength(30)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(ModuleModel))]
        public string Module_Key { get; set; }

        [MaxLength(128)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string BaseTable { get; set; }

        [MaxLength(76)]
        public string Category { get; set; }

        [MaxLength(76)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Title { get; set; }

        [MaxLength(76)]
        public string SubTitle { get; set; }

        [MaxLength(76)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string FileName { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(4096)]
        public string Fields { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(512)]
        public string ColumnWidths { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1024)]
        public string FieldsAlias { get; set; }

        [MaxLength(1024)]
        public string SortedColumns { get; set; }

        [MaxLength(1024)]
        public string GroupByColumns { get; set; }

        [MaxLength(1024)]
        public string PageBreakColumns { get; set; }

        [MaxLength(1024)]
        public string HiddenColumns { get; set; }

        [MaxLength(1024)]
        public string SumColumns { get; set; }

        [MaxLength(1024)]
        public string AvgColumns { get; set; }

        [MaxLength(1024)]
        public string MinColumns { get; set; }

        [MaxLength(1024)]
        public string MaxColumns { get; set; }

        [MaxLength(1024)]
        public string CountColumns { get; set; }

        [DataType("Integer")]
        [MaxLength(4)]
        public int TotalRan { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Run")]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? LastRanDate { get; set; }

        [MaxLength(4096)]
        public string Filter { get; set; }

        [MaxLength(4096)]
        public string FilterDescription { get; set; }

        //TODO: This is a function
        [MaxLength(153)]
        public string SearchField { get; set; }

        //TODO: Author is not in the db to model
        [Display(Name = "Author")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(AuthorModel))]
        public string Author_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ShowSelectionCriteria { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ShowGridLines { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ShowPageNumber { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ShowFileName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool HideDetails { get; set; }

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