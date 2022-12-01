using System.Net.Http.Json;
using OnlineStore.Models;

namespace OnlineStore.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;
    
    public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
    }
    
    
    
    
    // GET ALL PRODUCTS
    public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_all";
        var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>(uri,cts);
        return response!;
    }
    
    // GET PRODUCT BY ID
    public async Task<Product> GetProduct(Guid id,CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_by_id?id={id}";
        var response = await _httpClient.GetFromJsonAsync<Product>(uri,cts);
        return response!;
    }
    
    // ADD PRODUCT
    public async Task AddProduct(Product product,CancellationToken cts= default)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        var uri = $"{_host}/products/add";
        var response = await _httpClient.PostAsJsonAsync(uri, product, cts);
        response.EnsureSuccessStatusCode();
    }
    
    // UPDATE PRODUCT BY ID
    public async Task UpdateProduct(Guid id,Product product,CancellationToken cts= default)
    {
        var uri = $"{_host}/products/update/{id}";
        var response = await _httpClient.PutAsJsonAsync(uri, product,cts);
        response.EnsureSuccessStatusCode();
    }
    
    // DELETE ALL PRODUCT
    // public async Task Delete(CancellationToken cts= default)
    // {
    //     var uri = $"{_host}/products/delete";
    //     await _httpClient.DeleteAsync(uri,cts);
    // }
    
    // DELETE PRODUCT BY ID
    public async Task DeleteById(Guid id,CancellationToken cts= default)
    {
        var uri = $"{_host}/products/delete_by_id/{id}";
        var response = await _httpClient.DeleteAsync(uri,cts);
        response.EnsureSuccessStatusCode();
    }
    
    
    
    
}