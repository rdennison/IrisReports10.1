using System;

namespace IrisAttributes
{
    public sealed class HasChildGridAttribute : Attribute
    {
        public Type ChildModel { get; set; }
    }
}
