using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Exceptions
{
    public class NoUserException: Exception
    {
        public NoUserException() : base("User bulunamadı.")
        {
        }

        public NoUserException(string? message) : base(message)
        {
        }

        public NoUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
