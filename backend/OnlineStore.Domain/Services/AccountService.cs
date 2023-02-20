using OnlineStore.Data;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;


namespace OnlineStore.Domain.Services;

public class AccountService 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;

    public AccountService(IUnitOfWork unitOfWork,IPasswordHasherService? passwordHasherService, ITokenService? service)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasherService = passwordHasherService ?? throw new ArgumentNullException(nameof(passwordHasherService));
        _tokenService = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    
    public virtual async Task<(Account,string token)> RegisterAccount(
        string name,string email,string password, CancellationToken ctsToken = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        var hasherPassword =await _passwordHasherService.HashPassword(password);
        var emailName = await _unitOfWork.AccountRepository.FindByEmail(email, ctsToken);
        if ( emailName is null)
        {
            var account = new Account(name,email,hasherPassword,Guid.NewGuid());
            await _unitOfWork.AccountRepository.Add(account, ctsToken);
            var cart = new Cart() {Id = Guid.NewGuid(),AccountId = account.Id};
            await _unitOfWork.CartRepository.Add(cart, ctsToken);
            await _unitOfWork.CommitAsync(ctsToken);
            var token = _tokenService.GenerateToken(account);
            return (account,token);
        }
        throw new EmailAlreadyExists("This Email has already exists");
    }

    public virtual async Task<(Account,string token)> LoginAccount(string email, string password,CancellationToken cts = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        var account = await _unitOfWork.AccountRepository.FindByEmail(email,cts);
        if (account is null) throw new EmailNotFoundException(email);

        var result =_passwordHasherService.VerifyPassword(account.Password, password);
        if (result == null)
        {
            throw new WrongPasswordException("Invalid Password");
        }

        var token = _tokenService.GenerateToken(account);
        return (account,token);
    }

    public virtual async Task<IEnumerable<Account>> GetAccounts(CancellationToken cts = default)
    {
        return await _unitOfWork.AccountRepository.GetAll(cts);
    }

    public virtual async Task<Account> GetAccount(Guid id,CancellationToken cts = default)
    {
        return await _unitOfWork.AccountRepository.GetById(id,cts);
    }

}