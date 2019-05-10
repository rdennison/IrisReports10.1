using System;
namespace IrisAttributes
{
    public sealed class CantEditAttribute : Attribute
    {
        public bool ShowInEditor { get; set; }
        
    }
}
