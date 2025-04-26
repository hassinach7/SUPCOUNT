namespace SupCountBE.Core.Exceptions
{
    public class JustificationException : Exception
    {
        public JustificationException(string message) : base(message)
        {

        }
        public JustificationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
