using MovieSuggestion.Data.Utils.MovieSuggestion.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Core.Utils
{
    public static class MailUtils
    {
        public static void SendMail(string subject, string body, string mailTo)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(mailTo);
            mail.From = new MailAddress(EnvironmentVariable.GetConfiguration().MailFrom);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            var smtp = new SmtpClient
            {
                Host = EnvironmentVariable.GetConfiguration().SmtpHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EnvironmentVariable.GetConfiguration().MailFrom, EnvironmentVariable.GetConfiguration().SmtpPass)
            };
            try
            {
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
