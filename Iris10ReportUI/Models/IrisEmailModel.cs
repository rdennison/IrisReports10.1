using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IrisAttributes;

namespace Iris10ReportUI.Models
{
    public class IrisEmailModel
    {
        
        public string FromName { set; get; }
        public string FromEmail { get; set; }
      
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ToEmail { get; set; }
        

    }
}