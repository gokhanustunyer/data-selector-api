using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Abstractions.Services.Email;
using YGKAPI.Application.Exceptions.Email;

namespace YGKAPI.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmAccountEmailAsync(string userEmail, string subject, string url, bool isBodyHtml = true)
        {
            string body = $"<a href='{_configuration["ClientDomainHttp"] + url}' style='padding:1rem;outline:none;border:none;background-color:blue;text-color:white'>Doğrula</a>";
            await SendEmailAsync(new[] { userEmail }, subject, body, isBodyHtml);
        }

        public async Task SendConfirmAccountUpdateEmailAsync(string userEmail, string subject, string url, bool isBodyHtml = true)
        {
            string body = $"<a href='{_configuration["ClientDomainHttp"] + url}' style='padding:1rem;outline:none;border:none;background-color:blue;text-color:white'>Doğrula</a>";
            await SendEmailAsync(new[] { userEmail }, subject, body, isBodyHtml);
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendEmailAsync(new[] { to }, subject, body, isBodyHtml);
        }
        public async Task SendEmailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                MailMessage mail = new();
                mail.IsBodyHtml = isBodyHtml;
                foreach (var to in tos) mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new(_configuration["Mail:Username"], "YGK", Encoding.UTF8);

                SmtpClient smtp = new();
                smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Port = Int32.Parse(_configuration["Mail:Port"]);
                smtp.EnableSsl = bool.Parse(_configuration["Mail:EnableSSL"]);
                smtp.Host = _configuration["Mail:Host"];
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new MailSendException("email.mailSendException.message");
            }
        }
    }
}