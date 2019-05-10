using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportLibrary2.Models
{

    public class GroupViewModel
    {
        public string field { get; set; }
        public string dir { get; set; }
        public string[] aggregates { get; set; }
    }
}





