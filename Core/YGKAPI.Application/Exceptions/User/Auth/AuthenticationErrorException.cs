﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Exceptions.User.Auth
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Unexpected authentication Error")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}