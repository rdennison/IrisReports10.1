using Iris10ReportUI.Services;
using System;
using System.Threading.Tasks;

namespace Iris10ReportUI.Services
{

    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public bool SendEmailAsync(string email, string subject, string message)
        {
            GMailer mailer = new GMailer();
            mailer.ToEmail = email;
            mailer.Subject = subject;
            mailer.Body = message;
            mailer.IsHtml = true;
           var sender = mailer.Send();


       
            return sender;
        }

        public Task SendSmsAsync(string number, string message)
        {

            return Task.FromResult(0);
        }
    }
}
