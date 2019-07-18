using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IrisModels.Models;
using CoreDomain;

namespace Iris10ReportUI.GridBuilder.Extensions
{

     public sealed class UserNames
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public object BaseData { get; set; }
    }
    public class UserListHelper
    {
        public SelectList GenerateUserList()
        {
            CoreService _coreService = new CoreService();
            IList<IRISUserModel> userData = _coreService.LoadModel<IRISUserModel>().ToList();
            List<UserNames> uName = new List<UserNames>(); 

            foreach(var name in userData)
            {
                uName.Add(new UserNames { Value = name.Email, Text = name.UserName });
            }
            return new SelectList(uName, "Value", "Text");
        }
    }
}
