namespace OnlineStore.Domain.Exceptions;

public class HttpBadRequestException : Exception
{
    public HttpBadRequestException(string json) : base(message: json)
    {
       
    }
}