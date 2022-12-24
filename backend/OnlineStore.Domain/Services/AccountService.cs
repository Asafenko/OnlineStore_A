using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.RepositoriesInterfaces;

namespace OnlineStore.Domain.Services;

public class AccountService 
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;

    public AccountService(IAccountRepository accountRepository,IPasswordHasherService? passwordHasherService, ITokenService? service)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _passwordHasherService = passwordHasherService ?? throw new ArgumentNullException(nameof(passwordHasherService));
        _tokenService = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    
    public virtual async Task<(Account,string token)> RegisterAccount(
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
            var token = _tokenService.GenerateToken(account);
            return (account,token);
        }
        throw new EmailAlreadyExists("This Email has already exists");
    }

    public virtual async Task<(Account,string token)> LoginAccount(string email, string password,CancellationToken cts = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        var account = await _accountRepository.FindByEmail(email,cts);
        if (account is null) throw new EmailNotFoundException(email);

        var result =_passwordHasherService.VerifyPassword(account.Password, password);
        if (result == null)
        {
            throw new WrongPasswordException("Invalid Password");
        }

        var token = _tokenService.GenerateToken(account);
        return (account,token);
    }



    public async Task<Account> GetAccount(Guid id)
    {
        return await _accountRepository.GetById(id);
    }

}