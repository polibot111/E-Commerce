using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Exceptions
{
    public class PasswordChangeFaildException: Exception
    {
        public PasswordChangeFaildException() : base("Şifre güncellenirken bir sorun oluştu.")
        {
        }

        public PasswordChangeFaildException(string? message) : base(message)
        {
        }

        public PasswordChangeFaildException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
