using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisAPI2.Models
{
    public sealed class TimecardViewModel
    {
        public string Timecard_Key { get; set; }
        public string Activity_Key { get; set; }
        public DateTime Task_Date { get; set; }
        public bool FuelImport { get; set; }
        public int User_Key { get; set; }
        public string SecurityUser_Key { get; set; }
    }
}