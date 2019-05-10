using CoreDomain;
using IrisModels.API;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User;

namespace Iris10ReportUI.Attributes
{

    public class AuthenticateAttribute : FilterAttribute, IAuthorizationFilter
    {
        CoreService service = new CoreService();
        public string PermissionKey { get; set; }
        public int? PermissionLevel { get; set; }
        public string PermissionDescription { get; set; }
        public string PermissionGroup { get; set; }

        public AuthenticateAttribute() { }

        public AuthenticateAttribute(string permissionKey, int permissionLevel = 0)
        {
            PermissionKey = permissionKey;
            PermissionLevel = permissionLevel;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["IRIS_SESSION"];
            if (PerformAuthentication(cookie == null ? null : cookie.Value) == false)
            {
                filterContext.RequestContext.HttpContext.Response.Cookies["IRIS_SESSION"].Expires = DateTime.Now.AddMonths(-12);
                AuthorizationFailed(filterContext);
            }

        }

        public bool Emulate()
        {
            HttpContext current = HttpContext.Current;
            if (current == null) return false;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["IRIS_SESSION"];
            return PerformAuthentication(cookie == null ? null : cookie.Value);
        }

        //Fast menu iteration security check
        public bool MenuService(string authkey = "")
        {
            Dictionary<string, Guid> controllerGuids = (Dictionary<string, Guid>) HttpRuntime.Cache["ControllerGuids"];
            Guid currentGuid = Guid.Empty;
            AuthUserInformationModel userModel = (AuthUserInformationModel) HttpRuntime.Cache["CurrentUser"];
            try
            {
                if (controllerGuids.ContainsKey(authkey != "" ? authkey : PermissionKey) && userModel != null)
                {
                    currentGuid = controllerGuids[authkey != "" ? authkey : PermissionKey];
                    SecurityModel security = service.LoadModel<SecurityModel>(conName: HttpContext.Current.Session["ConString"].ToString()).FirstOrDefault(u => u.ObjectGUID == currentGuid.ToString() && u.User_Key == userModel.UserKey);

                    if (security?.SecurityLevel >= PermissionLevel)
                    {
                        return true;
                    }
                    else if (PermissionLevel == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (PermissionLevel == -1) //Allow for some screens to not require Security
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false; //something happened, menu item not available
            }
        }


        public bool PerformAuthentication(string sessionKey)
        {
            bool validSession = false;
            UserService userService = new UserService();
            if (sessionKey != null)
            {
                if (userService.ValidateSessionKey(sessionKey))
                {
                    // Read session info from database based on cookie value
                    // If wrapper to check if session existed and that the expiration of the session is still valid (>= DateTime.Now
                    // Load up user information from the user attached to the session
                    // Check to make sure user account is still valid (blocked? removed? etc ...)
                    // Load role/permission information for the user that has been loaded and build a combined "UserInformation" model for reference later
                    Dictionary<string, Guid> controllerGuids = (Dictionary<string, Guid>) HttpRuntime.Cache["ControllerGuids"];
                    Guid currentGuid = Guid.Empty;
                    AuthUserInformationModel userinfo = userService.GetAuthUserInformation(sessionKey);
                    userService.SetUserInformationForCurrentRequest(userinfo);
                   
                    try
                    {
                        if (controllerGuids.ContainsKey(PermissionKey))
                        {
                            currentGuid = controllerGuids[PermissionKey];
                            SecurityModel security = service.LoadModel<SecurityModel>(conName: HttpContext.Current.Session["ConString"].ToString()).FirstOrDefault(u => u.ObjectGUID == currentGuid.ToString() && u.User_Key == userinfo.UserKey);

                            if (security?.SecurityLevel >= PermissionLevel)
                            {
                                validSession = true;
                            }
                            else if(PermissionLevel == -1)
                            {
                                validSession = true;
                            }
                            else
                            {
                                validSession = false;
                            }
                        }else if(PermissionLevel == -1) //Allow for some screens to not require Security
                        {
                            validSession = true;
                        }
                        else
                        {
                            validSession = false;
                        }
                        
                    }
                    catch (Exception)
                    {
                        validSession = true; //temporary
                    }
                }
            }

            return validSession;
        }


        private void AuthorizationFailed(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult();

            filterContext.Result = new RedirectResult("~/App/Login");
        }

    }
}