using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace IrisAttributes
{
    public sealed class ForeignKeyReference
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public object BaseData { get; set; }
    }

    public sealed class ForeignKeyAttribute : Attribute
    {
        private static List<Type> serviceTypes = null;
        public static Type GetServiceTypeFor(Type objectType)
        {
            if (serviceTypes == null)
            {
                serviceTypes = new List<Type>();
                Assembly a = Assembly.Load("Iris10ReportUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                foreach (TypeInfo ti in a.DefinedTypes)
                {
                    if (ti.FullName.StartsWith("Iris10ReportUI.Services"))
                        serviceTypes.Add(ti.AsType());
                }
            }

            foreach (Type t in serviceTypes)
            {
                if (t.Name.EndsWith("Service") && t.BaseType.GenericTypeArguments.Length == 1 && t.BaseType.GenericTypeArguments[0] == objectType)
                    return t;
            }

            return null;
        }

        public Dictionary<string, string> Settings { get; set; }
        public string ForeignKeyDisplayField { get; set; }
        public Type ReferenceModelType { get; set; }
        public string AdditionalText { get; set; }
        public bool EnableFiltering { get; set; }

        public ForeignKeyAttribute(Type t) { ReferenceModelType = t;}

        public SelectList GetForeignKeyList()
        {
            Type serviceType = GetServiceTypeFor(ReferenceModelType);
            if (serviceType == null)
                return null;
            else
            {
                object service = serviceType.GetConstructor(new Type[0]).Invoke(null);
                MethodInfo m = serviceType.GetMethod("GetForeignKeyList");
                IEnumerable<ForeignKeyReference> references = m.Invoke(service, new object[] { this }) as IEnumerable<ForeignKeyReference>;
                IEnumerable<ForeignKeyReference> referenceList = references.OrderBy(o => o.Description);
                ForeignKeyReference blank = new ForeignKeyReference() { Key = null, Description = "" };
                List<ForeignKeyReference> selList = new List<ForeignKeyReference>();
                selList.Add(blank);
                selList.AddRange(referenceList);
                referenceList = selList;
                HttpContext.Current.Session[ReferenceModelType.Name.Replace("Model","_Key")] = new SelectList(referenceList, "Key", "Description");
                return new SelectList(referenceList, "Key", "Description");
            }
        }
    }
}
