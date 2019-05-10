﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAttributes
{
    public sealed class GridSetupAttribute : Attribute
    {
        public int GridHeight { get; set; }

        public bool Selectable { get; set; }
        //public GridSetupAttribute(int h)
        //{
        //    GridHeight = h;
        //}
    }
}
