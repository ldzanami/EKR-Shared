using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Exceptions
{
    [Serializable]
    public class ClientSideException : EKRException
    {
        public ClientSideException(string type) : base(type) { }

        public ClientSideException(string message, string type) : base(message, type) { }

        public ClientSideException(string message, Exception inner, string type) : base(message, inner, type) { }
    }
}
