using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Domain.Services;

namespace OnlineStore.WebApi.Controllers;

[Route("carts")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }


    [HttpGet("get_all")]
    public async Task<IEnumerable<Cart>> GetCarts(CancellationToken csToken)
    {
        return await _cartService.GetCarts(csToken);
    }



}