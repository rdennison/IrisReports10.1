using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class MeterReadingHistoryViewModel
    {

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Reading Date")]
        public DateTime? LastReadingDate { get; set; }

        [Display(Name = "Last PM Reading Hours")]
        public int LastReadingHours { get; set; }


        [Display(Name = "Last PM Reading Miles")]
        public int LastReadingMiles { get; set; }


    }
}