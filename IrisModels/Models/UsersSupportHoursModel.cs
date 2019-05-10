using IrisAttributes;
using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisModels.Models
{
    //Note: Might be child grid at some point
    [ModelDataBindings(DatabaseName = "AmazonDatabase", TableName = "UsersSupportHours", KeyFieldName = "UsersSupportHours_Key")]
    public sealed class UsersSupportHoursModel
    {
        [Key]
        [IsAutoNumber]
        public int UserSupportHours_Key { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Duration")]
        public double Duration { get; set; }

        [ForeignKey(typeof(UsersModel))]
        [Display(Name = "User Called")]
        public string CallUsers_Key { get; set; }

        [ForeignKey(typeof(IRISUserModel))]
        [Display(Name = "Call Taken By")]
        public string User_Key { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}
