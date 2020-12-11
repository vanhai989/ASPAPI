
using CRUDApi.EmailHelper;
using CRUDApi.Models;
using CRUDApi.Services.Impl;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        [Obsolete]
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        [Obsolete]
        private MimeMessage CreateEmailMessage(Message message)
        {
            MimeMessage emailMessage = new MimeMessage();
            try
            {
                emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
           
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
 