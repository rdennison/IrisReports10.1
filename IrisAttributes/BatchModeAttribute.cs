using System;

namespace IrisAttributes
{
    public class BatchModeAttribute : Attribute
    {
        public string DataBoundEvent { get; set; }
        public bool StartInEdit { get; set; }
    }
}
