using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.Services;
using OnlineStore.HttpModels.Requests;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.WebApi.Controllers;

// 
[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService account)
    {
        _accountService = account ?? throw new ArgumentNullException(nameof(account));
    }


    //accounts/register
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var (account,token) = await _accountService.RegisterAccount(
                request.Name, request.Email, request.Password, cancellationToken);
            return new RegisterResponse(account.Id,account.Name,account.Email,token);
        }
        catch (EmailAlreadyExists)
        {
            return BadRequest(new ErrorResponse("This Email has already exists"));
        }
    }

    //account/log_in
    [HttpPost("log_in")]
    public async Task<ActionResult<LogInResponse>> Login(LogInRequest request, CancellationToken cts = default)
    {
        try
        {
            var (account,token) = await _accountService.LoginAccount(request.Email, request.Password, cts);
            return new LogInResponse(account.Id,account.Name,account.Email,token);
        }
        catch (EmailNotFoundException)
        {
            return BadRequest(new ErrorResponse ("This Email was not found" ));
        }
        catch (WrongPasswordException)
        {
            return BadRequest(new ErrorResponse ("Invalid Password"));
        }
    }

    [Authorize]
    [HttpGet("get_current")]
    public async Task<ActionResult<Account>> GetCurrentAccount()
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (strId is null)
        {
            return Unauthorized();
        }
        var accountId = Guid.Parse(strId);
        var account = await _accountService.GetAccount(accountId);
        return account;
    }  
    
    

    //accounts/get_all
    //[HttpGet("get_all")]
    //public async Task<IEnumerable<Account>> GetAccounts(CancellationToken cts = default)
    // {
    //     var accounts = await _accountRepository.GetAll(cts);
    //     return accounts;
    // }

    //accounts/get_by_id
    //[HttpGet("get_by_id")]
    // public async Task<Account> GetById([FromQuery]Guid id,CancellationToken cts = default)
    // {
    //     var account = await _accountRepository.GetById(id, cts);
    //     return account;
    // }


    //accounts/update
    // [HttpPut("update")]
    // public async Task UpdateAccount([FromBody]Account account, CancellationToken cts = default)
    // {
    //     if (account == null) throw new ArgumentNullException(nameof(account));
    //     await _accountRepository.Update(account, cts);
    // }
    //
    // //accounts/get_by_name
    // [HttpGet("get_by_email")]
    // public async Task<Account> GetByEmail(string email,CancellationToken cts = default)
    // {
    //     if (email == null) throw new ArgumentNullException(nameof(email));
    //     var accountName = await _accountRepository.GetByEmail(email, cts);
    //     return accountName;
    //
    // }


    //accounts/delete
    // [HttpDelete("delete_by_id")]
    // public async Task DeleteById([FromQuery] Guid id,CancellationToken cts = default)
    // {
    //     await _accountRepository.DeleteById(id, cts);
    // }
}