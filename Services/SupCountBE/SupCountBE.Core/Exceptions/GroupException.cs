namespace SupCountBE.Core.Exceptions;

public class GroupException : Exception
{
    public GroupException(string message) : base(message)
    {
    }
    public GroupException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
