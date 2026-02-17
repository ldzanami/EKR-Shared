using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Exceptions
{
    public class ServerSideException : EKRException
    {
        public ServerSideException(string type) : base(type) { }

        public ServerSideException(string message, string type) : base(message, type) { }

        public ServerSideException(string message, Exception inner, string type) : base(message, inner, type) { }
    }
}
