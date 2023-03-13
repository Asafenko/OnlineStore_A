using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Services;

public interface IPasswordHasherService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string passwordHash, string password);
}