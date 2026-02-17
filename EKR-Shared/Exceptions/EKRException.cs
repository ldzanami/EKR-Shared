using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Exceptions
{
    [Serializable]
    public class EKRException : Exception
    {
        public string Type { get; set; }

        public EKRException(string type) => Type = type;

        public EKRException(string message, string type) : base(message)
        {
            Type = type;
        }

        public EKRException(string message, Exception inner, string type) : base(message, inner)
        {
            Type = type;
        }

        public override string ToString() => "{\n" +
                                            $"\tExceptionName: {Type}" +
                                            $"\tExceptionText: {Message}\n" +
                                             "}";
    }
}
