using CulinaryR3cipes.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Email
{
    public class SendGridEmailSender : ISendGridEmailSender
    {
        private IConfiguration _configuration;

        public SendGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMail(string token)
        {
            var apiKey = _configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("CulinaryR3cipes@example.com");
            var subject = "Potwierdzenia rejestracji";
            var to = new EmailAddress("hubert.kaszuba@gmail.com");
            var plainTextContent = "Potwierdź: ";
            var htmlContent = String.Format("<strong>{0}</strong>", token);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
