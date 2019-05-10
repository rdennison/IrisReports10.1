using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class TreeViewModel
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public List<TreeViewModel> Items { get; set; }
    }
}