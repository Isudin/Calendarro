using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Calendarro.Models.SendGird
{
    public class SendGridLogic : IEmailSender
    {
        //public void SendEmail(string receciver)
        //{

        //}

        static async Task Execute(string receciver)
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("raneckimateusz@gmail.com", "CalendarroTeam");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(receciver, "New User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email);

        }
    }
}
