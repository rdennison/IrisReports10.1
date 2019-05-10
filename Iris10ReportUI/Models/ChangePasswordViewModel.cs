using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris10ReportUI.Models
{
    public class ChangePasswordViewModel
    {

        public string User_Key { get; set; }
        public string UserName { get; set; }
        public string PasswordOne { get; set; }
        public string PasswordTwo { get; set; }
        public string OldPassword { get; set; }
        
        
    }
}
