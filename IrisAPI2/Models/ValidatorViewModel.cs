using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisAPI2.Models
{
    public sealed class ValidatorViewModel
    {
        public string API_Key { get; set; }
        public string County_Key { get; set; }
        public string DBName { get; set; }
        public List<string> AllowedGetContainers { get; set; }
        public List<string> AllowedSetContainers { get; set; }
    }
}