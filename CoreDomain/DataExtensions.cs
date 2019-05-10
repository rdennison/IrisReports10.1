using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Reflection;
using System.Web;

namespace CoreDomain
{
    public static class DataExtensions
    {
        public static DatabaseModelBindings GetDatabaseBindings(this object model) { return model.GetType().GetDatabaseBindings(); }
        public static DatabaseModelBindings GetDatabaseBindings(this Type modelType)
        {
            DatabaseModelBindings bindings = new DatabaseModelBindings();
            ModelDataBindingsAttribute bindingAttribute = modelType.GetCustomAttribute<ModelDataBindingsAttribute>(true);
            if (bindingAttribute == null) bindingAttribute = new ModelDataBindingsAttribute();

            if (string.IsNullOrEmpty(bindingAttribute.DatabaseName) == false)
                bindings.DatabaseName = bindingAttribute.DatabaseName;
            else
                bindings.DatabaseName = HttpContext.Current.Session["ConString"].ToString();

            if (string.IsNullOrEmpty(bindingAttribute.TableName) == false)
                bindings.TableName = bindingAttribute.TableName;
            else
            {
                string tableName = modelType.Name;
                if (tableName.EndsWith("Model", StringComparison.InvariantCultureIgnoreCase))
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                else if (tableName.EndsWith("DropDown", StringComparison.InvariantCultureIgnoreCase))
                    tableName = tableName.Remove(tableName.Length - 8, 8);

                bindings.TableName = tableName;
            }

            if (string.IsNullOrEmpty(bindingAttribute.KeyFieldName) == false)
                bindings.KeyFieldName = bindingAttribute.KeyFieldName;
            else
            {
                bindings.KeyFieldName = string.Format("{0}_Key", bindings.TableName);
            }

            return bindings;
        }
    }
}
