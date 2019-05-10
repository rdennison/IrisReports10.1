﻿using System;
namespace IrisAttributes
{
    public sealed class ApiSecurityAttribute : Attribute
    {
        public bool Pull { get; set; }
        public bool Push { get; set; }
        public bool NoAccess { get; set; } 
    }
}
