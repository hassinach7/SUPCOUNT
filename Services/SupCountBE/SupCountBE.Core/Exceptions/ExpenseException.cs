namespace SupCountBE.Core.Exceptions;

public class ExpenseException : Exception
{
    public ExpenseException(string message) : base(message)
    {

    }
    public ExpenseException(string message, Exception innerException) : base(message, innerException)
    {

    }
}
