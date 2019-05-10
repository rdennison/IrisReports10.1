using IrisAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace IrisAPI2.Controllers
{
    public class ObjectListController : ApiController
    {

        [HttpGet]
        public Dictionary<string, Guid> GetList()
        {
            Dictionary<string, Guid> objList = new Dictionary<string, Guid>();
            Assembly a = Assembly.Load("IrisAPI2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            foreach(TypeInfo ti in a.DefinedTypes)
            {
                if(ti.FullName.StartsWith("IrisAPI2.Helper") && !ti.Name.StartsWith("<"))
                {
                    if(ti.GetCustomAttribute<APIContainerAttribute>()?.DisplayName != null)
                    {
                        objList.Add(ti.GetCustomAttribute<APIContainerAttribute>()?.DisplayName, ti.GUID);
                    }
                }
            }

            return objList;
        }
        
    }
}
