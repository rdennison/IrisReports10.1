using IrisAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    [Report(NotReportable = true)]
    public sealed class ReportContentModel
    {
        [Key]
        [IsAutoNumber]
        public int ReportContent_Key { get; set; } //not sure what this model will contain yet
        

    }
}

