//using IrisWeb.Code.Data.Attributes;
using IrisAttributes;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FieldPropTemplate/Read", "~/API/FieldPropTemplate/Create", "~/API/FieldPropTemplate/Update", "~/API/FieldPropTemplate/Destroy")]
    [Report(NotReportable = true)]
    public sealed class FieldPropTemplateModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        //[IsAutoNumber]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(4)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string FieldPropTemplate_Key { get; set; }

        [MaxLength(76)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsEnabled { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsRequired { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowUserConfig { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool IsNumeric { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowNegative { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool AllowDecimal { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool Hidden { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool ValidateMe { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool Unbound { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string FieldType { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string FieldTypeTemplate { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool LeftOfDecimal { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(1)]
        [DataType("Boolean")]
        public bool RightOfDecimal { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Integer")]
        [MaxLength(4)]
        public int MaxLength { get; set; }

        [DataType("Integer")]
        [MaxLength(4)]
        public int Width { get; set; }
       
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool GridFieldOrder { get; set; }

        [MaxLength(20)]
        public string FieldFormat { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CalculatedField { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
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
    }
}