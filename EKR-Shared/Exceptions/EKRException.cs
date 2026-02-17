using EKR_Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Exceptions
{
    [Serializable]
    public class EKRException : Exception
    {
        public virtual string Type => EKRExceptionsText.UnexpectedServerSideError;

        public EKRException() { }

        public EKRException(string message) : base(message) { }

        public EKRException(string message, Exception inner) : base(message, inner) { }

        public override string ToString() => "{\n" +
                                            $"\tExceptionName: {Type}" +
                                            $"\tExceptionText: {Message}\n" +
                                            $"\tInner: {InnerException?.Message}" +
                                             "}";
    }
}
