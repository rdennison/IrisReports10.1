using System.Data;
using System.Web;
using IrisModels.API;
using IrisModels.Models;
using System.Collections.Generic;

namespace User
{
    public interface IUserService
    {
        AuthUserInformationModel GetAuthUserInformation(string sessionGuid);
        AuthUserInformationModel GetAuthUserInformation(string email, bool activeFlag);
        DataSet GetUserById(string userKey);
        DataTable GetUserInformation(string sessionGuid);
        DataTable GetUserInformation(string email, bool activeFlag);
        AuthUserInformationModel GetUserInformationForCurrentRequest();
        IRISUserModel GetUserModel(string email);
        IRISUserModel GetUserModel(int userKey);
        IRISUserModel GetUserModel(string searchField, string searchValue);
        bool IsAuthenticated();
        string ResetUserPassword(string userEmail);
        string ResetUserPassword(string userEmail, string newPassword = "");
        void SetUserInformationForCurrentRequest(AuthUserInformationModel userInfo);
        HttpCookie StartSessionCookie(string username, string password);
        void TerminateSession(string sessionToken);
        bool ValidateSecurityLevel(string viewName, int requiredMinLevel);

        bool ValidateSessionKey(string sessionGuid);
        IEnumerable<IRISUserModel> ListUserModel();
    }
}