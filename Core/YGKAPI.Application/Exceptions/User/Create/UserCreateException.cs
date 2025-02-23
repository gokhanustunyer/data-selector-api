using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Exceptions.User.Create
{
    public class UserCreateException : Exception
    {
        public UserCreateException() : base("Unexpected error while creating user")
        {
        }

        public UserCreateException(string? message) : base(message)
        {
        }

        public UserCreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}