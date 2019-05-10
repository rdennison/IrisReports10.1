using CoreDomain;
using IrisAPI2.Models;
using IrisAttributes;
using IrisModels;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IrisAPI2.Helper
{

    public class APIKeyValidator
    {
        private readonly CoreService _coreService = new CoreService();
        private string dataBase = "Initial Catalog=IRISSystemData;Data Source=173.12.189.227;User ID=developer;Password=aociris;MultipleActiveResultSets=True;";
        public ValidatorViewModel KeyValidator(string key)
        {
            ValidatorViewModel validator = new ValidatorViewModel();
            List<string> securityGetKeys = new List<string>();
            List<string> securitySetKeys = new List<string>();
            var objList = GetList();
            string countyKey = "SYS0000001";
            var userKey = _coreService.LoadModel<IRISUserModel>(null, 0, 0, countyKey, null, true, dataBase).FirstOrDefault(); //TODO: change this over to an api model
            var apiSecurityObjects = _coreService.LoadModel<APISecurityModel>(null, 0, 0, countyKey).Where(x => x.APIKey == key);
            //validator.API_Key = userKey.APIKey;
            //validator.County_Key = userKey.County_Key;
            validator.DBName = userKey.OriginDB;
            foreach(var item in apiSecurityObjects)
            {
                if(item.AccessLevel == 1 || item.AccessLevel == 3)
                    securityGetKeys.Add(objList.FirstOrDefault(x => x.Value == Guid.Parse(item.ObjectGUID)).Key);
                if (item.AccessLevel == 2 || item.AccessLevel == 3)
                    securitySetKeys.Add(objList.FirstOrDefault(x => x.Value == Guid.Parse(item.ObjectGUID)).Key);
            }
            validator.AllowedGetContainers = securityGetKeys;
            validator.AllowedSetContainers = securitySetKeys;

            return validator;

            
        }

        public Dictionary<string, Guid> GetList()
        {
            Dictionary<string, Guid> objList = new Dictionary<string, Guid>();
            Assembly a = Assembly.Load("IrisAPI2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            foreach (TypeInfo ti in a.DefinedTypes)
            {
                if (ti.FullName.StartsWith("IrisAPI2.Helper") && !ti.Name.StartsWith("<"))
                {
                    if (ti.GetCustomAttribute<APIContainerAttribute>()?.DisplayName != null)
                    {
                        objList.Add(ti.GetCustomAttribute<APIContainerAttribute>()?.DisplayName, ti.GUID);
                    }
                }
            }

            return objList;
        }
    }
}