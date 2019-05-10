using IrisAttributes;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class UserRightModel : ModelBase
    {
     
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "User Right")]
        public string UserRight_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "User")]
        [ForeignKey(typeof(IRISUserModel))]
        public string User_Key { get; set; }

        [Display(Name = "Page/Report")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string ObjectName { get; set; }

        [Display(Name = "Security Level")]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string SecurityLevel { get; set; }
    }
}
