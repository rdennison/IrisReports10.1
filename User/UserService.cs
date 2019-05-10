using System;
using System.Collections.Generic;
using System.Web;
using CoreDomain;
using IrisModels.API;
using IrisModels.Models;
using System.Linq;
using System.Net.Http;

namespace User
{
    public sealed class UserService
    {
        private const string C_SESSION_USER_INFO_KEY = "IRIS_USER_INFO";
        private const int C_SESSION_LIFESPAN = 15 * 60;
        private const int C_SALT_VALUE_SIZE = 16;
        private readonly CoreService _coreService = new CoreService();

        public bool IsAuthenticated()
        {
            return HttpContext.Current.Items[C_SESSION_USER_INFO_KEY] != null;
        }

        public void CreateTenantList()
        {
            IEnumerable<TenantModel> tenants = _coreService.LoadModel<TenantModel>(conName: "IrisAuth");
            Dictionary<int, string> tenantList = new Dictionary<int, string>();
            foreach(var tenant in tenants)
            {
                tenantList.Add(tenant.Tenant_Key, tenant.TenantName);
            }
            HttpContext.Current.Session["TenantList"] = tenantList; //TODO: was CountyList
        }

        public void SetUserInformationForCurrentRequest(AuthUserInformationModel userInfo)
        {
            HttpContext.Current.Items[C_SESSION_USER_INFO_KEY] = userInfo;
        }

        public AuthUserInformationModel GetUserInformationForCurrentRequest()
        {
            return (AuthUserInformationModel) HttpContext.Current.Items[C_SESSION_USER_INFO_KEY];
        }

        public bool ValidateSecurityLevel(string viewName, int requiredMinLevel)
        {
            AuthUserInformationModel userInfo = GetUserInformationForCurrentRequest();
            if (userInfo != null && userInfo.RoleLookup.ContainsKey(viewName))
            {
                if (userInfo.RoleLookup[viewName] >= requiredMinLevel)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a session from the system
        /// </summary>
        /// <param name="sessionToken"></param>
        public void TerminateSession(string sessionToken)
        {
            
            _coreService.Terminate<IRISUserModel>(sessionToken);
        }


        /// <summary>
        /// Starts a new session for the user and creates a new HttpCookie object with the 
        /// new session information for the user.
        /// </summary>
        public HttpCookie StartSessionCookie(string username, string password)
        {
            string newSessionCode;
            DateTime newSessionExpires;
            
            if (_coreService.StartSession<IRISUserModel>(username, password, out newSessionCode, out newSessionExpires))
            {
                HttpCookie cookie = new HttpCookie("IRIS_SESSION", newSessionCode);
                cookie.HttpOnly = true;
                cookie.Expires = newSessionExpires;

                return cookie;
            }

            return null;
        }

        /// <summary>
        /// Validate a session GUID string with the database to make sure a session exists
        /// for this GUID.s
        /// </summary>
        public bool ValidateSessionKey(string sessionGuid)
        {
            return _coreService.ValidateSessionKey(sessionGuid);
        }

        /// <summary>
        /// Get AuthUserInformationModel using the session GUID string
        /// </summary>
        public AuthUserInformationModel GetAuthUserInformation(string sessionGuid)
        {
            try
            {
                return BuildAuthUserInformationModel(GetUserInformation(sessionGuid));
            }
            catch { return null; }
        }

        /// <summary>
        /// Get AuthUserInformationmodel using the email and active state of the user account
        /// </summary>
        public AuthUserInformationModel GetAuthUserInformation(string email, bool activeFlag)
        {
            try
            {
                return BuildAuthUserInformationModel(GetUserInformation(email, activeFlag));
            }
            catch (Exception)
            {

            }
            return null;
        }

        private AuthUserInformationModel BuildAuthUserInformationModel(IRISUserModel userInfoTable)
        {
            AuthUserInformationModel userInfo = new AuthUserInformationModel();
            userInfo.UserKey = userInfoTable.User_Key;
            userInfo.Username = userInfoTable.UserName;
            userInfo.FullName = userInfoTable.NameDesc;
            userInfo.TenantKey = userInfoTable.DefaultTenant_Key;

            return userInfo;
        }
        
        
        public string ResetUserPassword(string userEmail)
        {
            var newPassword = "";
            
            try
            {                
                var userData = _coreService.LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.UserName == userEmail);
                
                if (userData != null)
                {
                    
                    string hashValue = "";
                    string salt = "";
                    newPassword = CryptoHelper.GeneratePassword();
                    CryptoHelper.ComputePassword(newPassword, out hashValue, out salt);
                    
                    HttpContext.Current.Session["ReturnedSalt"] = userData.SALT;
                    HttpContext.Current.Session["ReturnedHashPassword"] = userData.HashPassword;
                    userData.LoginChangePassword = false;
              
                    userData.SALT = salt;
                    userData.HashPassword = hashValue;
                    userData.LoginChangePassword = true;
                    
                    _coreService.SprocUpdate(userData, "IrisAuth");
                }
            }
            catch (Exception ex) 
            {
                newPassword = "";
            }

            return newPassword;

        }
        
        public IRISUserModel GetUserInformation(string email, bool activeFlag)
        {
            try
            {
                IRISUserModel user = _coreService.LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.UserName == email && u.Active == Convert.ToByte(activeFlag));
                return user;
            }
            catch (Exception)
            {

            }
            return null;
        }
        
        //public DataSet GetUserById(string userKey)
        //{
        //    SqlGenerator sqlGenSelect = new SqlGenerator(SqlGenerator.SqlTypes.Select, "User");
        //    sqlGenSelect.AddField("*");
        //    sqlGenSelect.AddWhereParameter(null, "User", "User_Key", userKey, SqlWhereComparison.SqlComparer.Equal, null);

        //    return SQLHelper.FetchDataSet(sqlGenSelect, "CountyDatabase");
        //}
        public string ResetUserPassword(string userEmail, string newPassword = "")
        {
            var userData = _coreService.LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.Email == userEmail);

            if (userData != null)
            {
                string hashValue = "";
                string salt = "";
                if (newPassword.Equals(""))
                {
                    newPassword = CryptoHelper.GeneratePassword();
                    userData.LoginChangePassword = true;
                }
                else
                {
                    userData.LoginChangePassword = false;                    
                }

                CryptoHelper.ComputePassword(newPassword, out hashValue, out salt);
                userData.HashPassword = hashValue;
                userData.SALT = salt;
                    
                _coreService.SprocUpdate(userData, "IrisAuth");
            }

            return newPassword;
        }


        public IRISUserModel GetUserInformation(string sessionGuid)
        {
            IRISUserModel user = _coreService.LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.AuthGUID == sessionGuid && u.AuthDate >= DateTime.Now);
            return user;
        }
    }
}
