//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    public sealed class SystemAuditModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        //[IsAutoNumber]
        [Key]
        [ScaffoldColumn(false)]
        public string SystemAudit_Key { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        [MaxLength(3)]
        //[IrisGridColumn(Width = 180)]
        public DateTime? Date { get; set; }

        [MaxLength(100)]
        [Display(Name = "Associated Table")]
        public string AssociatedTable { get; set; }

        [Display(Name = "Associated Key")]
        public int AssociatedKey { get; set; }

        [Display(Name = "User")]
        public int User_Key { get; set; }

        [Display(Name = "Original Data")]
        public object OriginalRow { get; set; }

        [Display(Name = "Execution SQL")]
        public string SQLExecuted { get; set; }

        [Display(Name = "Undo SQL")]
        public string SQLUndo { get; set; }
    }
}