namespace OnlineStore.Domain;

public class EmailAlreadyExists : Exception
{
    public EmailAlreadyExists(string message) : base(message)
    {
    }
}