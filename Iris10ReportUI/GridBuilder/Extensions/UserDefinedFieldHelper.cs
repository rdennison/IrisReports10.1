using CoreDomain;
using IrisModels.Models;
using System.Collections.Generic;
using System.Web;
using Iris10ReportUI.Models;
using Iris10ReportUI.Models;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public sealed class UserDefinedFieldHelper
    {
        CoreService service = new CoreService();
        Dictionary<string, UserDefinedFieldViewModel> userList = new Dictionary<string, UserDefinedFieldViewModel>();
        
        //TODO: add user key and county key to this model so that we can make unique lists
        public void GenerateUserDefinedLists()
        {
            IEnumerable<UserDefinedFieldModel> model = service.LoadModel<UserDefinedFieldModel>(conName: HttpContext.Current.Session["ConString"].ToString());
            foreach( var m in model)
            {
                if(m.ModelGUID != null && !userList.ContainsKey(m.ModelGUID))
                {
                    UserDefinedFieldViewModel viewModel = new UserDefinedFieldViewModel
                    {
                        User1Title = m.User1,
                        User1Type = m.User1Type,
                        User2Title = m.User2,
                        User2Type = m.User2Type,
                        User3Title = m.User3,
                        User3Type = m.User3Type,
                        User4Title = m.User4,
                        User4Type = m.User4Type,
                        User5Title = m.User5,
                        User5Type = m.User5Type,
                        User6Title = m.User6,
                        User6Type = m.User6Type,
                        User7Title = m.User7,
                        User7Type = m.User7Type,
                        User8Title = m.User8,
                        User8Type = m.User8Type,
                        User9Title = m.User9,
                        User9Type = m.User9Type,
                        User10Title = m.User10,
                        User10Type = m.User10Type
                    };
                    userList.Add(m.ModelGUID, viewModel);
                }
            }
            HttpContext.Current.Session["UserDefinedFields"] = userList;
        }
    }
}