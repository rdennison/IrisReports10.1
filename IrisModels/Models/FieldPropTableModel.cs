//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/FieldPropTable/Read", "~/API/FieldPropTable/Create", "~/API/FieldPropTable/Update", "~/API/FieldPropTable/Destroy")]
    public sealed class FieldPropTableModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string FieldPropTable_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(76)]
        public string TableName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        public string TableDescription { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool CRWPrimaryTable { get; set; }

        [Display(Name = "Module")]
        [MaxLength(30)]
        [Required(ErrorMessage = "Your {0} is required.")]
        //[ForeignKey(typeof(ModuleModel))]
        public string Module_Key { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }
    }
}