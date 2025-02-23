using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Exceptions.Email
{
    public class MailSendException : Exception
    {
        public MailSendException() : base("An error occurred while sending email\r\n")
        {
        }

        public MailSendException(string? message) : base(message)
        {
        }

        public MailSendException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}