using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Calendarro.Models.SendGird
{
    public class SendGridLogic
    {
        static async Task Execute(string receiver)
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("CalendarroTeam@gmail.com", "CalendarroTeam");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(receiver, "New User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
