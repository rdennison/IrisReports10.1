using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iris10ReportUI.Services
{
    public interface IEmailSender
    {
        bool SendEmailAsync(string email, string subject, string message);
    }
}
