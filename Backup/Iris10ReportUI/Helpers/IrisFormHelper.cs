using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using IrisAttributes;

namespace Iris10ReportUI.Helpers
{

    public sealed class IrisFormHelper<T> 
    {
      
         Type _modelType;

        public void IrisFormBuilder()
        {
           
            _modelType = typeof(T);

            foreach(PropertyInfo p in _modelType.GetProperties())
            {
                
                ForeignKeyAttribute fk = p.GetCustomAttribute<ForeignKeyAttribute>();
                if (fk != null)
                {
                    fk.GetForeignKeyList();
                }

            }
    

        
        }
    }
}