using System;

namespace IrisAttributes
{
    public class CopyDownAttribute : Attribute
    {
        public CopyDownAttribute(bool isCopyDown)
        {
            IsCopyDown = isCopyDown;
        }

        public bool IsCopyDown { get; }

    }
}
