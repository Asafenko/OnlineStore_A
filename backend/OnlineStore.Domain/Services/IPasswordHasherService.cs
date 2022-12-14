namespace OnlineStore.Domain.Services;

public interface IPasswordHasherService
{
    public Task<string>  HashPassword(string password);
    public Task<bool> VerifyPassword(string passwordHash, string password);
}