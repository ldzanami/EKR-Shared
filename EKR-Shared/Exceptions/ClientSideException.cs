using EKR_Shared.Data;

namespace EKR_Shared.Exceptions
{
    [Serializable]
    public class ClientSideException : EKRException
    {
        public override string Type => EKRExceptionsText.ClientSideException;

        public ClientSideException() : base() { }

        public ClientSideException(string message) : base(message) { }

        public ClientSideException(string message, Exception inner) : base(message, inner) { }
    }
}
