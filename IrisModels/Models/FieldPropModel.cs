//using IrisWeb.Code.Data.Attributes;
using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FieldProp/Read", "~/API/FieldProp/Create", "~/API/FieldProp/Update", "~/API/FieldProp/Destroy")]
    [Report(NotReportable = true)]
    public sealed class FieldPropModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string FieldProp_Key { get; set; }

        [Display(Name = "Field Prop Table")]
        [MaxLength(10)]
        [Required]
        //[ForeignKey(typeof(FieldPropTableModel))]
        public string FieldPropTable_Key { get; set; }

        //TODO: Foreign key model?
        [Display(Name = "Foreign")]
        [MaxLength(10)]
        //[ForeignKey(typeof(ForeignModel))]
        public string Foreign_Key { get; set; }

        [MaxLength(76)]
        [Required]
        public string FieldName { get; set; }

        [MaxLength(1024)]
        [Required]
        public string FullName { get; set; }

        [MaxLength(76)]
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsEnabled { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsRequired { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ForceResetSettings { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowUserConfig { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsNumeric { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowNegative { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowDecimal { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool Hidden { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ValidateMe { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool Unbound { get; set; }

        //TODO: missing FieldTypeTemplate table in db
        [Display(Name = "Field Type Template")]
        [MaxLength(100)]
        //[ForeignKey(typeof(FieldTypeTemplateModel))]
        public string FieldTypeTemplate_Key { get; set; }

        [MaxLength(20)]
        public string FieldType { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool LeftOfDecimal { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool RightOfDecimal { get; set; }

        [MaxLength(1)]
        [DataType("Boolean")]
        public bool RightOfDecimalDefault { get; set; }

        [MaxLength(1)]
        [DataType("Boolean")]
        public bool LeftOfDecimalDefault { get; set; }

        [Required]
        [MaxLength(4)]
        [DataType("Integer")]
        public int MaxLength { get; set; }
        
        [DataType("Integer")]
        [MaxLength(4)]
        public int Width { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool GridFieldOrder { get; set; }

        [MaxLength(20)]
        public string FieldFormat { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CalculatedField { get; set; }

        [Required]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CRWOnly { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CRWExclude { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CRWReverseJoin { get; set; }

        //TODO: Find this db table CRWLoadOnlyInModule
        [Display(Name = "CRW Load Only In Module")]
        [MaxLength(30)]
        //[ForeignKey(typeof(CRWLoadOnlyInModuleModel))]
        public bool CRWLoadOnlyInModule_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CRWLoadOnlyWhenBaseTable { get; set; }
        
        [DataType("Integer")]
        [MaxLength(2)]
        public int SortOrder { get; set; }

        [MaxLength(4)]
        public string SortIndicator { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string SortOrderSQL { get; set; }

        [MaxLength(512)]
        public string FieldDescription { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }
    }
}