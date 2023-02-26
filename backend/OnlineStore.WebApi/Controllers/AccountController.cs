using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;
using OnlineStore.HttpModels.Requests;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.WebApi.Controllers;


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
        CancellationToken ctsToken = default)
    {
        var (account,token) = await _accountService.RegisterAccount(
                request.Name, request.Email, request.Password, ctsToken);
            
            return new RegisterResponse(account.Id,account.Name,account.Email,token);
    }

    //account/log_in
    [HttpPost("log_in")]
    public async Task<ActionResult<LogInResponse>> Login(LogInRequest request, CancellationToken ctsToken = default)
    {
        var (account,token) = await _accountService.LoginAccount(request.Email, request.Password, ctsToken);
            return new LogInResponse(account.Id,account.Name,account.Email,token);
    }

    
    
    //accounts/get_all
    [HttpGet("get_all")]
    public async Task<IEnumerable<Account>> GetAccounts(CancellationToken ctsToken = default)
    {
        var accounts = await _accountService.GetAccounts(ctsToken);
        return accounts;
    }

    
    
    
    [Authorize]
    [HttpGet("get_current")]
    public async Task<ActionResult<Account>> GetCurrentAccount(CancellationToken ctsToken = default)
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (strId is null)
        {
            return Unauthorized();
        }
        var accountId = Guid.Parse(strId);
        var account = await _accountService.GetAccount(accountId,ctsToken);
        return account;
    } 
    
    
    [Authorize(Roles = $"{Roles.Admin}")]
    [HttpGet("get_admin")]
    public IActionResult GetAllAdminAccounts()
    {
        return Ok();
    }
    
    
    
    // [Authorize]
    // [HttpPost("logout")]
    // public IActionResult LogOut()
    // {
    //     return Ok();
    // }
    
    
    
    [HttpDelete("delete_all")]
    public async Task DeleteAll(CancellationToken ctsToken)
    {
        await _accountService.Delete(ctsToken);
    }
}