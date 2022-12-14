using OnlineStore.Domain.Entities;

namespace OnlineStore.BlazorClient.BasketShop;

public interface ICartService
{
    IReadOnlyList<Product> GetShopProduct();
    Task AddToCart(Guid id, CancellationToken cts);
}