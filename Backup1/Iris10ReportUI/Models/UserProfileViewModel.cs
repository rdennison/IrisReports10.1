using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris10ReportUI.Models
{
   public class UserProfileViewModel
    {
        public string User_Key { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public byte[] Signature { get; set; }

        public byte[] ProfilePicture { get; set; }
 
        

    }
}
