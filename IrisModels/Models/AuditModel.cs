using IrisAttributes;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisModels.Models
{
    public class AuditModel : ModelBase
    {
        [Key]
        [ScaffoldColumn(false)]
        [MaxLength(10)]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        public string Audit_Key { get; set; }

        [Display(Name = "Audit Table")]
        public string AssociatedTable { get; set; }

        [Display(Name = "Audit Table Key")]
        public string AssociatedKey { get; set; }

        [Display(Name = "Action Description")]
        public string ActionDescription { get; set; }

        [Display(Name = "New Content")]
        public string NewContent { get; set; }

        [Display(Name = "User Key")]
        public int User_Key { get; set; }

        [Display(Name = "Date Time Stamp")]
        public DateTime DateTimeStamp { get; set; }


    }
}
