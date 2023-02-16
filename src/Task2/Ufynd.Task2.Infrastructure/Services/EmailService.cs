using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using Ufynd.Task2.Application.Common.Settings;
using Ufynd.Task2.Application.Contracts.Infrastructure;

namespace Ufynd.Task2.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptionsMonitor<ReportSettingsConfigurationModel> _reportSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptionsMonitor<ReportSettingsConfigurationModel> reportSettings, ILogger<EmailService> logger)
        {
            _reportSettings = reportSettings;
            _logger = logger;
        }

        public bool SendEmailWithAttachment(string recipient, string attachmentFilePath)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_reportSettings.CurrentValue.SenderEmail);
                message.To.Add(new MailAddress(recipient));
                message.Subject = "Sending file by AutoProcess Service";
                message.Body = "Best regards, U:fync team";

                Attachment attachment = new Attachment(attachmentFilePath);
                message.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient(_reportSettings.CurrentValue.SmtpClientHost, _reportSettings.CurrentValue.SmtpClientPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_reportSettings.CurrentValue.SenderEmail, _reportSettings.CurrentValue.SenderPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
