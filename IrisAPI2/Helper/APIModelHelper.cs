using IrisAttributes;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisAPI2.Helper
{
    public sealed class APIModelHelper
    {
       
        [APIContainer(typeof(RISCulvertModel), DisplayName="Culverts")]
        public class CulvertModelObject
        {
            [APIPrimaryModel(typeof(RISCulvertModel))]
            public RISCulvertModel CulvertModel { get; set; }
            //[APIForeignRelation(typeof(RISCulvertAuditModel), typeof(RISCulvertModel))]
            //public RISCulvertAuditModel CulvertAuditModel { get; set; }
            [APIForeignRelation(typeof(RISCulvertHistoryModel), typeof(RISCulvertModel))]
            public RISCulvertHistoryModel CulvertHistoryModel { get; set; }
            //[APIForeignRelation(typeof(RISCulvertHistoryAuditModel), typeof(RISCulvertModel))]
            //public RISCulvertHistoryAuditModel CulvertHistoryAuditModel { get; set; }
        }

        [APIContainer(typeof(TransactModel), DisplayName = "Transact")]
        public class TransactModelObject
        {
            [APIPrimaryModel(typeof(TransactModel))]
            public TransactModel TransactModel { get; set; }
            ////[APIForeignRelation(typeof(RISCulvertAuditModel), typeof(RISCulvertModel))]
            ////public RISCulvertAuditModel CulvertAuditModel { get; set; }
            //[APIForeignRelation(typeof(RISCulvertHistoryModel), typeof(RISCulvertModel))]
            //public RISCulvertHistoryModel CulvertHistoryModel { get; set; }
            ////[APIForeignRelation(typeof(RISCulvertHistoryAuditModel), typeof(RISCulvertModel))]
            ////public RISCulvertHistoryAuditModel CulvertHistoryAuditModel { get; set; }
        }


        //[APIContainer(typeof(RISSignModel), DisplayName = "Signs")]
        //public class SignModelObject
        //{
        //    public RISSignModel SingModel { get; set; }
        //    [APIForeignRelation(typeof(RISSignHistoryModel), typeof(RISSignModel))]
        //    public RISSignHistoryModel SignHistoryModel { get; set; }
        //}
    }

}
