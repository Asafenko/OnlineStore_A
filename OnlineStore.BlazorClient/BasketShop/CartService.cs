using OnlineStore.HttpApiClient;
using OnlineStore.Models;

namespace OnlineStore.BlazorClient.BasketShop;

public class CartService : ICartService
{
    
    private readonly List<Product> _shopProduct = new();
    
    private readonly IShopClient _client;
    
    public CartService(IShopClient client)
    { 
        _client = client;
    }
    public  IReadOnlyList<Product> GetShopProduct()
    {
       return _shopProduct;
    }

    public async Task AddToCart(Guid id,CancellationToken cts)
    {
        var addProduct = await _client.GetProduct(id, cts);
        _shopProduct.Add(addProduct);
    }

}