using CI_Platform.Entity.Models;
using CI_Platform.Service.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailsettings;

        public EmailService(IOptions<EmailSetting> options)
        {
            this.emailsettings = options.Value;
        }
        public async Task<bool> SendEmailAsync(MailRequest mailrequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailsettings.Email);
                email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
                email.Subject = mailrequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailrequest.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(emailsettings.Host, emailsettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailsettings.Email, emailsettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
