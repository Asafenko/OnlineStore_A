namespace OnlineStore.HttpModels.Responses;

public record RegisterResponse(Guid AccountId,string Name,string Email,string Token);