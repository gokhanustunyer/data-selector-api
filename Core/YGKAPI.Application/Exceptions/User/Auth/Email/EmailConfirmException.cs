using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Exceptions.User.Auth.Email
{
    public class EmailConfirmException : Exception
    {
        public EmailConfirmException() : base("Email is not confirmed")
        {
        }

        public EmailConfirmException(string? message) : base(message)
        {
        }

        public EmailConfirmException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}