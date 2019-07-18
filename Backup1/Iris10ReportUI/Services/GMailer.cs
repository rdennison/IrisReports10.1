using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreDomain;
using IrisModels.Models;
using System.Net.Mail;

namespace Iris10ReportUI.Services
{
    public class GMailer
    {
        private readonly CoreService _coreService = new CoreService();


        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp-relay.gmail.com";
            //GmailHost = "10.0.0.40";
            GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public bool Send(string userMessage = "")
        {

            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential();

            //using (var message = new MailMessage("173.12.189.227", ToEmail))
            using (var message = new MailMessage("devserver.lgc@oregoncounties.org", ToEmail))

            {


                //Boolean ErrorFlag = false;

                try
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    smtp.Send(message);
                }

               catch (Exception ex)
                {
                    var userData = _coreService.LoadModel<IRISUserModel>().FirstOrDefault(u => u.UserName == ToEmail);
                    userData.SALT = HttpContext.Current.Session["ReturnedSalt"].ToString();
                    userData.HashPassword = HttpContext.Current.Session["ReturnedHashPassword"].ToString();

                    userData.LoginChangePassword = false;


                    //ErrorFlag = true;
                    //ModelState.ReferenceEquals(string.Empty, "Email server cannot be found at this time.");
                    return false;
                }

                return true;




            }
        }
    }
}