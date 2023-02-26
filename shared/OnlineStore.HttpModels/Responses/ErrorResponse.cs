namespace OnlineStore.HttpModels.Responses;

public record ErrorResponse(string Message)
{
    public override string ToString()
    {
        return $"{{ Message= {Message} ,StatusCode= }}";
    }
}
