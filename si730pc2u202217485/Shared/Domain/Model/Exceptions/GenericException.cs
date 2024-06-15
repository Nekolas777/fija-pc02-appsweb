namespace si730pc2u202217485.Shared.Domain.Model.Exceptions;

public class GenericException : Exception
{
    public GenericException(string message) : base(message)
    {
    }
    
    public GenericException(string message, Exception innerException) : base(message, innerException)
    {
    }
}