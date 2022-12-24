namespace OnlineStore.Domain.Exceptions;

public class EmailAlreadyExists : Exception
{
    public EmailAlreadyExists(string? message) : base(message)
    {
    }
}