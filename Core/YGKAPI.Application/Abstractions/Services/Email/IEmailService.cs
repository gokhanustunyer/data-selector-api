using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Abstractions.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendConfirmAccountEmailAsync(string userEmail, string subject, string url, bool isBodyHtml = true);
        Task SendConfirmAccountUpdateEmailAsync(string userEmail, string subject, string url, bool isBodyHtml = true);
        Task SendEmailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
    }
}
