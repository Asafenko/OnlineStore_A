using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;

namespace OnlineStore.WebApi.Services;

public class Pbkdf2PasswordHasher : IPasswordHasherService
{
    private readonly PasswordHasher<Account> _hasher;
    private readonly Account _dummy;//new Account("", "", "", new Guid())

    
    public Pbkdf2PasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor)
    {
        if (optionsAccessor == null) throw new ArgumentNullException(nameof(optionsAccessor));
        _hasher = new PasswordHasher<Account>(optionsAccessor);
    }

    public async Task<string> HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        var hasherPassword = _hasher.HashPassword(_dummy, password);
        return hasherPassword;
    }

    public async Task<bool> VerifyPassword(string passwordHash, string providedPassword)
    {
        if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
        if (providedPassword == null) throw new ArgumentNullException(nameof(providedPassword));
        var result = _hasher.VerifyHashedPassword(_dummy, passwordHash, providedPassword);
        return result is not PasswordVerificationResult.Failed;
    }
}