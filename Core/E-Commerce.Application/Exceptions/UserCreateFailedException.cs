﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException(): base("Kullanıcı oluşturulurken beklenmeyen bir hata ile karşılaşıldı.")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
