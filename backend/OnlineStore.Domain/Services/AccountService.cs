using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Domain.Services;

public class AccountService 
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public AccountService(IAccountRepository accountRepository,IPasswordHasherService? passwordHasherService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _passwordHasherService = passwordHasherService ?? throw new ArgumentNullException(nameof(passwordHasherService));
    }
    public virtual async Task<Account> RegisterAccount(
        string name,string email,string password, CancellationToken cts = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));

        // ApiController
        // if (!ModelState.IsValid)
        // {
        //     return ValidationProblem(ModelState);
        // }
        var hasherPassword =await _passwordHasherService.HashPassword(password);
        var emailName = await _accountRepository.FindByEmail(email, cts);
        if ( emailName is null)
        {
            var account = new Account(name,email,hasherPassword,Guid.NewGuid());
            await _accountRepository.Add(account, cts);
            return account;
        }
        throw new EmailAlreadyExists("This Email has already exists");
    }

    public virtual async Task<bool> CheckAccountHash(string password,string providedPassword)
    {
        var answer = await _passwordHasherService.VerifyPassword(password, providedPassword);
        return answer;
    }
    


}