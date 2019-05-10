using IrisAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreDomain.ViewModels
{
    public sealed class ReportRelationshipViewModel
    {
        public string ModelGUID { get; set; }
        public string Prop { get; set; }
        public string ToModelGUID { get; set; }
        public string ToProp { get; set; }
        public string JoinType { get; set; }

    }
}

