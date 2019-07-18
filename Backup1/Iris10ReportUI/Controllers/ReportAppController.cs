using IrisModels.API;
using Iris10ReportUI.Attributes;
using Iris10ReportUI.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Iris10ReportUI.Models;
using CoreDomain;
using IrisModels.Models;
using IrisAttributes;
using System.Text.RegularExpressions;
using Iris10ReportUI.GridBuilder.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iris10ReportUI.Helpers;
using Iris10ReportUI.Attributes;

namespace Iris10ReportUI.Controllers.Manage
{
    public class ReportAppController : RootController
    {
        readonly User.UserService _userService = new User.UserService();
        private readonly CoreService _coreService = new CoreService();
        private AuthUserInformationModel _currentUser;
        private readonly PageNameHelper pageHelper = new PageNameHelper();

        // Root landing page once a user has logged into the application
        [Route("")]
        [Authenticate("ReportApp", -1)]
        public ActionResult ReportMain()
        {
            HttpCookie cookie = Request.Cookies["PageCookie"];
            HttpCookie usercookie = Request.Cookies["UserCookie"];

            if (Session["CurrentUserName"] != null)
            {
                if (cookie != null)
                {
                    Response.Cookies.Remove("PageCookie");
                    Session.Remove("ModelType");
                }
                return View();
            }
            else
            {
                _currentUser = _userService.GetUserInformationForCurrentRequest();

                if (usercookie == null)
                {
                    HttpCookie UserCookie = new HttpCookie("UserCookie");
                    UserCookie.Name = "UserCookie";
                    UserCookie.Value = _currentUser.Username;
                    UserCookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Set(UserCookie);
                }
                try
                {
                    if (!string.IsNullOrEmpty(cookie?.Value) && (usercookie?.Value == _currentUser.Username))
                    {
                        string[] val = cookie.Value.Split('/');
                        if (val.Length > 1)
                            return RedirectToAction(val[1], val[0]);
                        else
                            return RedirectToAction(val[0], val[0]);
                    }
                    else
                    {
                        HttpCookie UserCookie = new HttpCookie("UserCookie");
                        UserCookie.Name = "UserCookie";
                        UserCookie.Value = _currentUser.Username;
                        UserCookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Set(UserCookie);
                        if (cookie != null)
                        {
                            Response.Cookies.Remove("PageCookie");
                            Session.Remove("ModelType");
                        }
                        return View();
                    }
                }
                catch (Exception)
                {
                    return View();
                }
            }
        }

        #region Login

        public async Task GenerateLists()
        {
            ReportAttribute reportLists = new ReportAttribute();
            SecurityAttribute security = new SecurityAttribute();
            //security.GenerateGuidList();
            //security.GenerateModelGuidList();
            //security.GenerateUIGridList();
            reportLists.GenerateLists();
        }

        /// <summary>
        /// Initial Login Controller Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            #region //Security object
            var t = Task.Run(async () => {
                await GenerateLists();
            });
            t.Wait();
            #endregion
            return View(new AuthStartRequestModel());
        }

        /// <summary>
        /// Post Login Controller Action
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(AuthStartRequestModel model, string ReturnUrl)
        {
            Dictionary<int, string> tenantList = (Dictionary<int, string>) Session["TenantList"];
            if (ModelState.IsValid)
            {
                var userInfo = _coreService.LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.UserName == model.Username);
                if (userInfo != null)
                {
                    var hashPassword = CryptoHelper.ComputeHash(model.Password, userInfo.SALT);
                    if (userInfo.HashPassword != hashPassword)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email and/or password.");
                        return View("Login", model);
                    }
                    if (userInfo.LoginChangePassword.HasValue ? !userInfo.LoginChangePassword.Value : false)
                    {

                        HttpCookie sessionCookie = _userService.StartSessionCookie(model.Username, model.Password);
                        if (sessionCookie != null)
                        {
                            Session["DefaultTenantKey"] = userInfo.DefaultTenant_Key;
                            Session["CurrentTenantKey"] = userInfo.DefaultTenant_Key;
                            Session["CurrentGeneralAccessLevel"] = userInfo.GeneralAccessLevel;
                            Session["CurrentUserKey"] = userInfo.User_Key;
                            Session["CurrentUserName"] = userInfo.UserName;
                            Session["ConString"] = "User" + userInfo.DefaultTenant_Key.ToString();
                            pageHelper.CreateTables();
                            Response.Cookies.Set(sessionCookie);
                     
                          
                            if (string.IsNullOrEmpty(ReturnUrl))
                                return RedirectToAction(actionName: "ReportMain", controllerName: "ReportApp");
                            else
                                return Redirect(ReturnUrl);
                        }
                        else
                        {

                            ModelState.AddModelError(string.Empty, "Invalid email and/or password.");
                            return View("Login", model);
                        }
                    }
                    else
                    {

                        if (Session["ExpirationTime"] != null && (DateTime) Session["ExpirationTime"] < DateTime.Now)
                        {
                            ModelState.AddModelError(string.Empty, "Your temporary password has expired.  Click the Forgot Your Password link to receive a new one.");
                            LostPasswordModel expiredPassword = new LostPasswordModel();
                            expiredPassword.Email = model.Username;
                            expiredPassword.FirstName = userInfo.FirstName;
                            ForgotPassword(expiredPassword);

                            return View("Login", model);

                        }
                        else
                        {
                            var IRISUserModel = new ChangePasswordViewModel { UserName = model.Username };
                            return RedirectToAction("ChangePassword", "ReportApp", IRISUserModel);
                        }
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Invalid email and/or password.");
                    return View("Login", model);
                }
            }

            return View("ReportMain", model);
        }


        [HttpPost]
        [Route("FailedLogin")]
        public ActionResult FailedLogin(AuthStartRequestModel model, string ReturnUrl)
        {

            ViewData["LoginMessage"] = ModelState.Last().Value;
            return Login(model, ReturnUrl);
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["IRIS_SESSION"];
            if (cookie != null)
            {
                _userService.TerminateSession(cookie.Value);
                cookie.Expires = DateTime.UtcNow.AddYears(-1);
            }
            return RedirectToAction("Login", "ReportApp");
        }

        #endregion


        #region Password

        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(LostPasswordModel model)
        {

            //Boolean ErrorFlag = false; //unused


            if (TryValidateModel(model))
            {
                var messageService = new AuthMessageSender();
                var userService = new User.UserService();
                var newPassword = userService.ResetUserPassword(model.Email);
                var sentmessage = messageService.SendEmailAsync(model.Email, "Reset IRIS Password", "Hello " + model.Email + " your temporary password is " + newPassword);

                if (!sentmessage)
                {
                    //ErrorFlag = true;
                    ModelState.AddModelError(string.Empty, "SMTP server is down, unable to send temporary password at this time.");

                    return View("ForgotPassword", model);
                }

                else
                {
                    Session["ExpirationTime"] = DateTime.Now.AddHours(4);

                    return View("ForgotPasswordConfirmation");
                }
                //return RedirectToAction("ForgotPasswordConfirmation"); //unreachable
            }



            else
            {
                return View("Login");
            }


        }


        [Route("ForgotPasswordConfirmation")]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel model, string userMessage)
        {

            var userInfo = _coreService.LoadModel<IRISUserModel>().FirstOrDefault(u => u.UserName == model.UserName);
            // PasswordScore score;
            //score = CheckStrength(model.PasswordOne);

            int minLen = 8;
            int maxLen = 30;
            int minDigit = 1;
            int minSpChar = 1;
            int minCapLetters = 1;

            Boolean ErrorFlag = false;
            //Check for password length
            if (model.PasswordOne.Length < minLen)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password must be at least " + minLen + " characters long.");
            }





            if (model.PasswordOne.Length > maxLen)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password must not exceed " + maxLen + " characters long.");
            }

            //Check for Digits and Special Characters
            int digitCount = 0;
            int splCharCount = 0;
            int capLetterCount = 0;
            bool excludedSpcCharacter = false;
            foreach (char c in model.PasswordOne)
            {
                if (char.IsDigit(c)) digitCount++;
                if (Regex.IsMatch(c.ToString(), @"[!#$%&*+-:<>?\\^_`|~]")) splCharCount++;
                if (Regex.IsMatch(c.ToString(), @"[A-Z]")) capLetterCount++;

                if (Regex.IsMatch(c.ToString(), @"^[.;\@`']") && !excludedSpcCharacter)
                {
                    ErrorFlag = true;
                    ModelState.AddModelError(string.Empty, "The following special characters cannot be used in a password." + "." + ";" + "`" + "'" + "@");
                    excludedSpcCharacter = true;
                }
            }

            if (capLetterCount < minCapLetters)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password must have at least " + minCapLetters + " capital letter.");
            }

            if (digitCount < minDigit)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password must have at least " + minDigit + " digit(s).");
            }
            if (splCharCount < minSpChar)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password must have at least " + minSpChar + " special character(s).");
            }

            if (model.PasswordOne.Contains("abcdef") || model.PasswordTwo.Contains("123456"))
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password cannot be a squence of numbers or letters");

            }

            if (model.PasswordOne == model.UserName || model.PasswordTwo == model.UserName)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Password cannot be the same as User Name");

            }



            if (model.PasswordOne != model.PasswordTwo)
            {
                ErrorFlag = true;
                ModelState.AddModelError(string.Empty, "Passwords do not match.");

            }
            else if (ErrorFlag)
                return View(model);
            else
            {

                var userService = new User.UserService();
                var newPassword = userService.ResetUserPassword(model.UserName, model.PasswordOne);
                if (newPassword != "")
                    return RedirectToAction("Login", new AuthStartRequestModel());
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Updating Password.");
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion




    }
}