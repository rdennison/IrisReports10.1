using CoreDomain;
using IrisAPI2.Models;
using IrisAPI2.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static IrisAPI2.Helper.APIModelHelper;

namespace IrisAPI2.Controllers
{
    public class Posting
    {
        public string Key { get; set; }
        public string Verb { get; set; }
        public string Data { get; set; }
    }

    public class GisController : ApiController
    {
        private readonly CoreService _coreService = new CoreService();
        private string dataBase = "Initial Catalog=dbName;Data Source=173.12.189.227;User ID=developer;Password=aociris;MultipleActiveResultSets=True;";
        private APIKeyValidator validator = new APIKeyValidator();

        [HttpGet]
        public IEnumerable<GisViewModel> GetAllGis(string filter, string key)
        {
            var user = validator.KeyValidator(key);
            List<GisViewModel> gis = new List<GisViewModel>();

            string countyKey = user.County_Key;// "SYS0000001";
            string dbName = user.DBName;// "A_Wallowa9";
            dataBase = dataBase.Replace("dbName", dbName);
            if(user.AllowedGetContainers.Any(s => s.Equals(filter, StringComparison.OrdinalIgnoreCase)))
            {
                switch (filter)
                {
                    case "culverts":
                        //var culverts = _coreService.LoadModel<RISCulvertModel>(null, 0, 0, countyKey, null, true, dataBase);
                        var culvertInfo = _coreService.LoadDataSet<CulvertModelObject>(countyKey, dataBase, true);

                        //culvertInfo.Tables["RISCulvertModel"].Rows[0].ItemArray[culvertInfo.Tables["RISCulvertModel"].Columns["RISCulvert_Key"].Ordinal]
                        for (int i = 0; i < culvertInfo.Tables.Count; i++)
                        {
                            if (culvertInfo.Tables[i].TableName == "RISCulvertModel")
                            {
                                for (int b = 0; b < culvertInfo.Tables[i].Rows.Count; b++)
                                {
                                    double x, y, z;
                                    double.TryParse(culvertInfo.Tables[i].Rows[b].ItemArray[culvertInfo.Tables[i].Columns["XCoordinate"].Ordinal].ToString(), out x);
                                    double.TryParse(culvertInfo.Tables[i].Rows[b].ItemArray[culvertInfo.Tables[i].Columns["YCoordinate"].Ordinal].ToString(), out y);
                                    double.TryParse(culvertInfo.Tables[i].Rows[b].ItemArray[culvertInfo.Tables[i].Columns["ZCoordinate"].Ordinal].ToString(), out z);
                                    GisViewModel g = new GisViewModel
                                    {
                                        Model_Key = culvertInfo.Tables[i].Rows[b].ItemArray[culvertInfo.Tables[i].Columns["RISCulvert_Key"].Ordinal].ToString(),
                                        Name = culvertInfo.Tables[i].Rows[b].ItemArray[culvertInfo.Tables[i].Columns["User1"].Ordinal].ToString(),
                                        Description = "No Description in DB",
                                        XCoord = x,
                                        YCoord = y,
                                        ZCoord = z
                                    };
                                    gis.Add(g);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
           

            return gis;
        }
        
        [HttpGet]
        public IHttpActionResult SetGis(string key, string verb, string data)
        {
            string msg = "";

            switch (verb)
            {
                case "UPDATE":
                    break;
                case "CREATE":
                    break;
                case "DELETE":
                    break;
                default:
                    msg = "verb must be CREATE, UPDATE, or DELETE";
                    break;
            }
            return Ok(msg);
        }
    }
}
