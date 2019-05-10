﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IrisAttributes
{
    public sealed class ReportFilterScreenReference
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public object BaseData { get; set; }
    } 

    public class ReportFilterDropdownAttribute : Attribute
    {
        public enum SqlWhereAndor
        {
            And = 0,
            OR = 1
        }

        public enum BooleanList
        {
            False = 0,
            True = 1
        }

        public enum SqlComparer
        {
            None = 0,
            Equal = 1,
            Like = 65,
            GreaterThan = 2,
            LessThan = 4,
            NotEqual = 6,
            NotLike = 70,
            BitwiseAnd = 8,
            BitwiseOr = 16,
            BitwiseExclusiveOr = 32,
            Partial = 64,
            Between = 100,
            Outside = 150,
            In = 200,
            Contains = 63,
            StartsWith = 53,
            EndsWith = 43
        }

        readonly static Dictionary<SqlComparer, string> comparerLookup = new Dictionary<SqlComparer, string>()
        {
            { SqlComparer.Equal, "=" },
            { SqlComparer.NotEqual, "<>" },
            { SqlComparer.GreaterThan, ">" },
            { SqlComparer.LessThan, "<" },
            { SqlComparer.GreaterThan | SqlComparer.Equal, ">=" },
            { SqlComparer.LessThan | SqlComparer.Equal, "<=" },
            { SqlComparer.BitwiseAnd, "&" },
            { SqlComparer.BitwiseOr, "|" },
            { SqlComparer.BitwiseExclusiveOr, "^" },
            { SqlComparer.BitwiseAnd | SqlComparer.Equal, "&=" },
            { SqlComparer.BitwiseOr | SqlComparer.Equal, "|=" },
            { SqlComparer.BitwiseExclusiveOr | SqlComparer.Equal, "^=" },
            { SqlComparer.Like, "LIKE" },
            { SqlComparer.NotLike, "NOT LIKE" },
            { SqlComparer.Between, "BETWEEN"},
            { SqlComparer.Outside, "OUTSIDE"},
            { SqlComparer.In, "IN"},
            { SqlComparer.Contains, "CONTAINS" },
            { SqlComparer.StartsWith, "STARTS WITH" },
            { SqlComparer.EndsWith, "ENDS WITH" }
        };

        readonly static Dictionary<SqlWhereAndor, string> andorLookup = new Dictionary<SqlWhereAndor, string>()
        {
            { SqlWhereAndor.And, "AND" },
            { SqlWhereAndor.OR, "OR" }
        };

        readonly static Dictionary<string, string> groupOneLookup = new Dictionary<string, string>()
        {
            {" ", " " },
            {"(", "(" }

        };

        readonly static Dictionary<string, string> groupTwoLookup = new Dictionary<string, string>()
        {
            {" ", " " },
            { ")", ")" }
        };

        readonly static Dictionary<BooleanList, string> truefalseLookup = new Dictionary<BooleanList, string>()
        {
            { BooleanList.True, "True" },
            { BooleanList.False, "False" }
        };

        public bool Description { get; set; }

        public bool Operator { get; set; }

        public bool Value1 { get; set; }

        public bool Value2 { get; set; }

        public bool Operator2 { get; set; }

        public bool Group1 { get; set; }

        public bool Group2 { get; set; }

        public Type ReferenceModelType { get; set; }

        public ReportFilterDropdownAttribute(Type t) { ReferenceModelType = t; }

        public ReportFilterDropdownAttribute() { }

        public SelectList GetFilterDropdownList(Type t)
        {
            Type myType = t;
            int count = 0;
            IList<PropertyInfo> fields = new List<PropertyInfo>(myType.GetProperties());
            IList<string> fieldNames = new List<string>();
            IList<PropertyInfo> fields1 = new List<PropertyInfo>();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();
            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();

            foreach (PropertyInfo p in fields)
            {
                if (p.GetCustomAttribute<FilterTypeAttribute>() != null)
                {
                    fieldNames.Add(p.GetCustomAttribute<DisplayAttribute>().Name);
                    fields1.Add(p);
                }
            }

            foreach (var f in fieldNames)
            {
                ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                references1.Key = fields1[count].Name;
                count++;
                references1.Description = f;
                referenceList.Add(references1);
            }

            referenceList1 = referenceList.OrderBy(o => o.Description);
            return new SelectList(referenceList1, "Key", "Description");
        }

        //TODO: TE Need to make sure the Cache is not an empty list
        public SelectList CreateReportFilterDropdownList()
        {
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();
            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            string props = HttpContext.Current.Session["ReportSelectedValues"]?.ToString();
            if (props != null)
            {
                foreach (var item in props.Split(','))
                {
                    ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                    references1.Key = item;
                    references1.Description = item;
                    referenceList.Add(references1);
                }
            }
            referenceList1 = referenceList.OrderBy(o => o.Description);
            return new SelectList(referenceList1, "Key", "Description");
        }

        public SelectList OperatorDropdownList(string c)
        {

            Dictionary<SqlComparer, string> fields = comparerLookup;

            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();
            switch (c)
            {
                case "Dropdown":
                    foreach (var f in fields)
                    {
                        if (f.Value.ToString() == "=" || f.Value.ToString() == ">=" || f.Value.ToString() == "<=" || f.Value.ToString() == "<>" || f.Value.ToString() == "BETWEEN" || f.Value.ToString() == "STARTS WITH" ||
                            f.Value.ToString() == "ENDS WITH" || f.Value.ToString() == "CONTAINS" || f.Value.ToString() == "LIKE" || f.Value.ToString() == "NOT LIKE")
                        {
                            ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                            references1.Key = f.Key.ToString();
                            references1.Description = f.Value;
                            referenceList.Add(references1);
                        }

                    }
                    break;

                case "Dropdown2":
                    foreach (var f in fields)
                    {
                        if (f.Value.ToString() == "=")
                        {
                            ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                            references1.Key = f.Key.ToString();
                            references1.Description = f.Value;
                            referenceList.Add(references1);
                        }

                    }
                    break;

                case "Date":
                    foreach (var f in fields)

                    {
                        if (f.Value.ToString() == "=" || f.Value.ToString() == ">=" || f.Value.ToString() == "<=" || f.Value.ToString() == "<>" || f.Value.ToString() == "BETWEEN")
                        {
                            ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                            references1.Key = f.Key.ToString();
                            references1.Description = f.Value;
                            referenceList.Add(references1);
                        }

                    }
                    break;

                case "Text":
                    foreach (var f in fields)
                    {
                        if (f.Value.ToString() == "STARTS WITH" ||
                            f.Value.ToString() == "ENDS WITH" || f.Value.ToString() == "CONTAINS" || f.Value.ToString() == "LIKE" || f.Value.ToString() == "NOT LIKE")
                        {
                            ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                            references1.Key = f.Key.ToString();
                            references1.Description = f.Value;
                            referenceList.Add(references1);
                        }

                    }
                    break;



            }



            references.Key = "KeyOperator";
            references.Description = references.Key;
            references.BaseData = fields;

            referenceList1 = referenceList;
            return new SelectList(referenceList1, "Key", "Description");

        }

        public SelectList AndOrDropdownList()
        {

            Dictionary<SqlWhereAndor, string> fields = andorLookup;

            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();

            foreach (var f in fields)
            {
                ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                references1.Key = f.Key.ToString();
                references1.Description = f.Value;
                referenceList.Add(references1);
            }
            references.Key = "KeyOperator";
            references.Description = references.Key;
            references.BaseData = fields;

            referenceList1 = referenceList;
            return new SelectList(referenceList1, "Key", "Description");

        }

        public SelectList GroupOneDropdownList()
        {

            Dictionary<string, string> fields = groupOneLookup;

            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();

            foreach (var f in fields)
            {
                ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                references1.Key = f.Value;
                references1.Description = f.Key.ToString();
                referenceList.Add(references1);
            }
            references.Key = "KeyOperator";
            references.Description = references.Key;
            references.BaseData = fields;

            referenceList1 = referenceList;
            return new SelectList(referenceList1, "Key", "Description");

        }

        public SelectList GroupTwoDropdownList()
        {

            Dictionary<string, string> fields = groupTwoLookup;

            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();

            foreach (var f in fields)
            {
                ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                references1.Key = f.Key.ToString();
                references1.Description = f.Value;
                referenceList.Add(references1);
            }
            references.Key = "KeyOperator";
            references.Description = references.Key;
            references.BaseData = fields;

            referenceList1 = referenceList;
            return new SelectList(referenceList1, "Key", "Description");

        }

        public SelectList TrueFalseList()
        {

            Dictionary<BooleanList, string> fields = truefalseLookup;

            IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
            ReportFilterScreenReference references = new ReportFilterScreenReference();
            IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();

            foreach (var f in fields)
            {
                ReportFilterScreenReference references1 = new ReportFilterScreenReference();
                references1.Key = f.Key.ToString();
                references1.Description = f.Value;
                referenceList.Add(references1);
            }
            references.Key = "KeyOperator";
            references.Description = references.Key;
            references.BaseData = fields;

            referenceList1 = referenceList;
            return new SelectList(referenceList1, "Key", "Description");

        }
    }
}
