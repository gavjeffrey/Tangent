using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangent.Web.Services
{
    /// <summary>
    /// Not implemeted - I commented out this app service is startup and removed all constructor injections of it
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
