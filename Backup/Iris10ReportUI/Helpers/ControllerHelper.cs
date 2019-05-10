using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using System.Web;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using CoreDomain;
using SqlComponents;
using Iris10ReportUI.GridBuilder.Extensions;
using System.Web.Script.Serialization;
using System.Linq;

using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
using Iris10ReportUI.Hubs;
using Iris10ReportUI.Models;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Iris10ReportUI.Helpers
{
    public class ControllerHelper
    {

        private readonly CoreService _coreService = new CoreService();
        public IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        public string CreateUpdate<T>([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<T> data)
        {
            string keyName = GetPrimaryKey(data);
            string retVal = "";
            string db = HttpContext.Current.Session["ConString"].ToString();
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            if (data != null)
            {
                List<T> tList = new List<T>();
                foreach (var item in data)
                {
                    tList.Add(keyHelper.KeysToNull(item));
                }
                data = tList;
                foreach (var member in data)
                {
                    if (member.GetType().GetProperty(keyName).GetValue(member) != null && int.Parse(member.GetType().GetProperty(keyName).GetValue(member).ToString()) != 0)
                    {
                        retVal = _coreService.SprocUpdate(member, db);
                    }
                    else
                    {
                        retVal = _coreService.SprocInsert(member, db);
                    }
                }
            }
            return retVal;
        }

        public string CreateUpdate<T>([DataSourceRequest]DataSourceRequest request, T data)
        {
            string keyName = GetPrimaryKey(data);
            string retVal = "";
            string db = HttpContext.Current.Session["ConString"].ToString();
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            if (data != null)
            {
                data = keyHelper.KeysToNull(data);
                if (data.GetType().GetProperty(keyName).GetValue(data) != null && int.Parse(data.GetType().GetProperty(keyName).GetValue(data).ToString()) != 0)
                {
                    retVal = _coreService.SprocUpdate(data, db);
                }
                else
                {
                    retVal = _coreService.SprocInsert(data, db);
                }
            }
            return retVal;
        }

        public IEnumerable<T> Destroy<T>([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<T> data)
        {
            string db = HttpContext.Current.Session["ConString"].ToString();
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            if (data != null)
            {
                List<T> tList = new List<T>();
                foreach (var item in data)
                {
                    tList.Add(keyHelper.KeysToNull(item));
                }
                data = tList;
                foreach (var member in data)
                {
                    _coreService.SprocDelete(member, db);
                }
            }
            return data;
        }

        public T Destroy<T>([DataSourceRequest]DataSourceRequest request, T data)
        {
            string db = HttpContext.Current.Session["ConString"].ToString();
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            if (data != null)
            {
                data = keyHelper.KeysToNull(data);
                _coreService.SprocDelete(data, db);
            }
            return data;
        }

        public DataSourceResult Read<T>([DataSourceRequest]DataSourceRequest request, List<SqlWhere> specialWheres = null)
        {
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            List<SqlWhere> myWheres = new List<SqlWhere>();
            List<string> groups = new List<string>();
            string db = HttpContext.Current.Session["ConString"].ToString();
            if (HttpContext.Current.Session["GridFilterWheres"] != null)
            {
                myWheres.AddRange((List<SqlWhere>) HttpContext.Current.Session["GridFilterWheres"]);
            }

            if (HttpContext.Current.Session["Searches"] != null)
            {
                List<SqlWhere> searchRequest = (List<SqlWhere>) HttpContext.Current.Session["Searches"];
                searchRequest.First().Andor = SqlWhereAndOrOptions.SqlWhereAndOr.And;
                searchRequest.First().Group1 = "(";
                searchRequest.Last().Group2 = ")";
                myWheres.AddRange((HttpContext.Current.Session["Searches"] != null) ? (List<SqlWhere>) HttpContext.Current.Session["Searches"] : null);
            }
            if (specialWheres != null)
                myWheres = specialWheres;
            var totalRecords = _coreService.RecordCount<T>(myWheres);
            if (request.Groups.Count > 0)
            {
                foreach (var g in request.Groups)
                {
                    groups.Add(g.Member);
                }
            }
            var data = _coreService.LoadModel<T>(myWheres, request.PageSize, request.Page, groups: groups, conName:db);
            //Iterate through keys and if you find a null value set it to an empty string, needed for kendo dropdown edits for incell editing
            List<T> tList = new List<T>();
            foreach (var item in data)
            {
                tList.Add(keyHelper.FixKeys(item));
            }
            request.Page = 1;
            DataSourceResult result = tList.ToDataSourceResult(request);
            result.Total = totalRecords;
            return result;
        }

        public DataSourceResult Read<T>([DataSourceRequest]DataSourceRequest request, bool special, List<SqlWhere> specialWheres = null)
        {
            List<SqlWhere> myWheres = new List<SqlWhere>();
            string db = HttpContext.Current.Session["ConString"].ToString();
            List<T> favs = new List<T>();
            List<T> avail = new List<T>();
            List<T> disabled = new List<T>();
            List<T> megaList = new List<T>();
            //List 1 for favorites
            //List 2 for access lists
            //List 3 for disabled reports
            
            if (specialWheres != null)
                myWheres = specialWheres;
            
            var data = _coreService.LoadModel<T>(wheres: myWheres, conName: db, orderfield: "ReportName");

            foreach(var item in data)
            {
                if((item as RPTReportListByUserModel).Favorite == true)
                {
                    favs.Add(item);
                }else if ((item as RPTReportListByUserModel).ReadOnlyAccess == true)
                {
                    avail.Add(item);
                }
                else
                {
                    disabled.Add(item);
                }
            }
            megaList.AddRange(favs);
            megaList.AddRange(avail);
            megaList.AddRange(disabled);
            
            DataSourceResult result = megaList.ToDataSourceResult(request);
            return result;
        }

        public void AutoSave<T>(T model, string row)
        {
            List<T> models = new List<T>();
            string db = HttpContext.Current.Session["ConString"].ToString();
            models.Add(model);
            IEnumerable<T> data = models;
            string keyName = GetPrimaryKey(data);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            KeyHelper<T> keyHelper = new KeyHelper<T>();
            settings.NullValueHandling = NullValueHandling.Ignore;

            JsonConvert.PopulateObject(row, model, settings);

            model = keyHelper.KeysToNull(model);

            
            if (model.GetType().GetProperty(keyName).GetValue(model) != null && model.GetType().GetProperty(keyName).GetValue(model).ToString() != "")
            {
                _coreService.SprocUpdate(model, db);
            }
            else
            {
                _coreService.SprocInsert(model, db);
            }
        }

        

        public AggregateViewModel GetAggs<T>(string field, string type)
        {
            string db = HttpContext.Current.Session["ConString"].ToString();
            List<SqlWhere> myWheres = new List<SqlWhere>();
            if (HttpContext.Current.Session["GridFilterWheres"] != null)
                myWheres.AddRange((List<SqlWhere>) HttpContext.Current.Session["GridFilterWheres"]);

            if (HttpContext.Current.Session["Searches"] != null)
            {
                List<SqlWhere> searchRequest = (List<SqlWhere>) HttpContext.Current.Session["Searches"];
                searchRequest.First().Andor = SqlWhereAndOrOptions.SqlWhereAndOr.And;
                searchRequest.First().Group1 = "(";
                searchRequest.Last().Group2 = ")";
                myWheres.AddRange((HttpContext.Current.Session["Searches"] != null) ? (List<SqlWhere>) HttpContext.Current.Session["Searches"] : null);
            }
            AggregateViewModel aggModel = new AggregateViewModel();
            aggModel.Type = type;
            aggModel.Field = field;
            aggModel.Result = _coreService.Aggregate<T>(type, field, myWheres, conName:db);
            return aggModel;
        }

        private string GetPrimaryKey<T>([Bind(Prefix = "models")]IEnumerable<T> data)
        {
            string keyName = "";
            
            foreach (var member in data)
            {
                foreach(var prop in member.GetType().GetRuntimeProperties())
                {
                    if(prop.GetCustomAttribute<KeyAttribute>() != null)
                    {
                        keyName = prop.Name;
                        break;
                    }
                }
            }
            return keyName;
        }

        private string GetPrimaryKey<T>(T data)
        {
            string keyName = "";

            foreach (var prop in data.GetType().GetRuntimeProperties())
            {
                if (prop.GetCustomAttribute<KeyAttribute>() != null)
                {
                    keyName = prop.Name;
                    break;
                }
            }
            return keyName;
        }
    }
}