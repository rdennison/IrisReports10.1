using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSReadingHistory_Key", TableName = "EMSReadingHistory")]
    public class ReadingHistoryViewModel
    {
        public string EMSReadingHistory_Key { get; set; }

        [Display(Name = "Reading Date")]
        public string LastReadingDate { get; set; }

        [Display(Name = "Reading Source")]
        public string ReadingSource { get; set; }

        [Display(Name = "Hours")]
        public string LastReadingHours { get; set; }

        [Display(Name = "Miles")]
        public string LastReadingMiles { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }





    }
}