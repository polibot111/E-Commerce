using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Exceptions
{
    public class EndpointFailedException:Exception
    {
        public EndpointFailedException() : base("Endpoint hatası.")
        {
        }

        public EndpointFailedException(string? message) : base(message)
        {
        }

        public EndpointFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
