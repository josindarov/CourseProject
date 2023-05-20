namespace Application.Exceptions;

public class NullException : Exception
{
    public NullException() 
        : base()
    {
        
    }

    public NullException(string message)
        :base(message)
    {
        
    }
}