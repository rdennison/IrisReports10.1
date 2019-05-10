﻿using System;

namespace IrisAttributes
{
    public sealed class HasParentAttribute : Attribute
    {
        public string ParentName { get; set; }

        public HasParentAttribute(string p) { this.ParentName = p; }

    }
}
