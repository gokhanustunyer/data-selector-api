using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Exceptions.User.Create
{
    public class PasswordMatchException : Exception
    {
        public PasswordMatchException() : base("Passwords is not matching")
        {
        }

        public PasswordMatchException(string? message) : base(message)
        {
        }

        public PasswordMatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}