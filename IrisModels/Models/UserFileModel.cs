//using IrisWeb.Code.Data.Attributes;
//using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
       //[IrisGrid("~/API/UserFile/Read", "~/API/UserFile/Create", "~/API/UserFile/Update", "~/API/UserFile/Destroy")]

    public sealed class UserFileModel
    {
        //[IsAutoNumber]
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(4)]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string UserFile_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "Associated Table")]
        public string AssociatedTable { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "Associated Key")]
        [MaxLength(4)]
        public int AssociatedKey { get; set; }
      
        [Display(Name = "Server File Path")]
        [MaxLength(-1)]
        public string ServerSidePath { get; set; }

    }
}