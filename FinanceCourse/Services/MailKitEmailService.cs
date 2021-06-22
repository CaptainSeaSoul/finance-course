using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebPortfolio.Services
{
    public class MailKitEmailService : IEmailSender
    {
        private readonly EmailServerConfiguration _eConfig;

        public MailKitEmailService(EmailServerConfiguration config)
        {
            _eConfig = config;
        }

        private async Task SendAsync(MimeMessage message)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_eConfig.SmtpServer, _eConfig.SmtpPort, true);

                await client.AuthenticateAsync(_eConfig.SmtpUsername, _eConfig.SmtpPassword);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEmailAsync(string subject, string content, string address, string name)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(name, address));
            message.From.Add(new MailboxAddress(_eConfig.EmailOwnerName, _eConfig.SenderEmailAddress));

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = content;
            message.Body = bodyBuilder.ToMessageBody();

            await SendAsync(message);
        }

        // Email to self
        public async Task SendEmailAsync(string subject, string content)
        {
            await SendEmailAsync(subject, content, _eConfig.SenderEmailAddress, _eConfig.EmailOwnerName);
        }

        // Email to User with no name (for IEmailSender)
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendEmailAsync(subject, htmlMessage, email, "User");
        }

        // Send with attachment 
        public async Task SendEmailAsync(string email, string subject, string htmlMessage, string[] attachments)
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress("User", email));
            message.From.Add(new MailboxAddress(_eConfig.EmailOwnerName, _eConfig.SenderEmailAddress));

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlMessage;

            foreach (var att in attachments)
                bodyBuilder.Attachments.Add(att);

            message.Body = bodyBuilder.ToMessageBody();

            await SendAsync(message);
        }
    }
}
