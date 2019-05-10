
using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FilterDetail/Read", "~/API/FilterDetail/Create", "~/API/FilterDetail/Update", "~/API/FilterDetail/Destroy")]
    [EditorType(InCell=true)]
    [BatchMode(StartInEdit = true)]
    [GridSetup(GridHeight = 200, Selectable = true)]
    [NoAutoSync]
    public sealed class FilterDetailModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string FilterDetail_Key { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string FilterType { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string TableName { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string FilterName { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [ScaffoldColumn(false)]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        public string FieldName { get; set; }
        
        [Display(Name = "(")]
        //[FilterDropdown(Group1 = true)]
        [IrisGridColumn(Width = 20)]
        public string Group1 { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        //[FilterDropdown(Description = true)]
        [IrisGridColumn(Width = 80)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        //[FilterDropdown(Operator = true)]
        [Display(Name = "Operator")]
        [IrisGridColumn(Width = 80)]
        public string Operator1 { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Value 1")]
        [IrisGridColumn(Width = 80)]
        //[FilterDropdown(Value1 = true)]
        public string Value1 { get; set; }
        
        //[FilterDropdown(Value2 = true)]
        [Display(Name = "Value 2")]
        [IrisGridColumn(Width = 80)]
        public string Value2 { get; set; }
        
        [Display(Name = ")")]
        //[FilterDropdown(Group2 = true)]
        [IrisGridColumn(Width = 20)]
        public string Group2 { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        //[FilterDropdown(Operator2 = true)]
        [Display(Name = "AND/OR")]
        [IrisGridColumn(Width = 50)]
        public string Operator2 { get; set; }
        
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string DataType { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        [DataType("Integer")]
        public int KeyField { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [DataType("Boolean")]
        [ScaffoldColumn(false)]
        public bool UseDropdown { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [DataType("Boolean")]
        [ScaffoldColumn(false)]
        public bool UseNonUniqueList { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        [DataType("Boolean")]
        public bool UseLikeOperator { get; set; }
        
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string SprocName { get; set; }
        
        [DataType("Integer")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public int RecordOrder { get; set; }
        
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string EnglishWhere { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ScaffoldColumn(false)]
        public string ForeignKey { get; set; }
    }
}