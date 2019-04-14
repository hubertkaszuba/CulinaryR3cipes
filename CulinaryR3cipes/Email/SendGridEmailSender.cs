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

        public async Task SendMail(string email, string subject, string message)
        {
            var apiKey = _configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("CulinaryR3cipes@example.com");
            var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(email), subject, message, message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
