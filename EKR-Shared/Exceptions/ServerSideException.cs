using EKR_Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Exceptions
{
    public class ServerSideException : EKRException
    {
        public override string Type => EKRExceptionsText.ServerSideException;

        public ServerSideException() : base() { }

        public ServerSideException(string message) : base(message) { }

        public ServerSideException(string message, Exception inner) : base(message, inner) { }
    }
}
