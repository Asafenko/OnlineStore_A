using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.HttpModels.Requests;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;
    private bool IsAuthorizationTokenSet { get; set; }
    
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
    public async Task AddProduct(Product product,CancellationToken cts = default)
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
    public async Task UpdateProduct(Guid id,Product product,CancellationToken cts = default)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        var uri = $"{_host}/products/update?id={id}";
        var response = await _httpClient.PutAsJsonAsync(uri, product,cts);
        response.EnsureSuccessStatusCode();
    }
    
    // DELETE PRODUCT BY ID
    public async Task DeleteById(Guid id,CancellationToken cts = default)
    {
        var uri = $"{_host}/products/delete_by_id?id={id}";
        var response = await _httpClient.DeleteAsync(uri,cts);
        response.EnsureSuccessStatusCode();
    }

    
    
    public  async Task<RegisterResponse> Registration(RegisterRequest request, CancellationToken ctsToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        var uri = $"{_host}/accounts/register";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri,request,ctsToken);
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
        { 
            var json = await responseMessage.Content.ReadAsStringAsync(ctsToken);
            throw new HttpBadRequestException(json);
        }
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<RegisterResponse>(
            cancellationToken: ctsToken);
        SetAuthorizationToken(response.Token,ctsToken);
        return response;
    }

    public async Task<LogInResponse> Login(LogInRequest request, CancellationToken ctsToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var uri = $"{_host}/accounts/log_in";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri,request,ctsToken);
        if(responseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            var json = await responseMessage.Content.ReadAsStringAsync(ctsToken);
            throw new HttpBadRequestException(json);
        }
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<LogInResponse>(
            cancellationToken: ctsToken);
        SetAuthorizationToken(response.Token,ctsToken);
        return response;
    }

    
    public void SetAuthorizationToken(string token,CancellationToken ctsToken = default)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));
         _httpClient.DefaultRequestHeaders.Authorization 
            = new AuthenticationHeaderValue("Bearer", token);
         IsAuthorizationTokenSet = true;
    }

    

    public void ResetAuthorizationToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
             IsAuthorizationTokenSet = false;
    }

    public async Task<Account> GetAccount(CancellationToken ctsToken = default)
    {
        var uri = $"{_host}/account/get_current";
        var response = await _httpClient.GetFromJsonAsync<Account>(uri,ctsToken);
        return response;
    }

    // public async Task<LogInResponse> GetCurrent(CancellationToken ctsToken = default)
    // {
    //     var uri = $"{_host}/account/get_current";
    //     var responseMessage = await _httpClient.PostAsJsonAsync(uri,ctsToken, cancellationToken: ctsToken);
    //     if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
    //     {
    //         var json = await responseMessage.Content.ReadAsStringAsync(ctsToken);
    //         throw new HttpBadRequestException(json);
    //     }
    //     responseMessage.EnsureSuccessStatusCode();
    //     var response = await responseMessage.Content.ReadFromJsonAsync<LogInResponse>(
    //         cancellationToken: ctsToken);
    //     return response;
    // }

    
    
    
    
    
    
    
}