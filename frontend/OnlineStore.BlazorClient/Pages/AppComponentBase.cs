using System.ComponentModel;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineStore.HttpApiClient;

namespace OnlineStore.BlazorClient.Pages;

public abstract class AppComponentBase : ComponentBase
{
     private readonly IShopClient Shopclient;
     private readonly ILocalStorageService _localStorageService;


    protected AppComponentBase(IShopClient shopClient,ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        Shopclient = shopClient ?? throw new ArgumentNullException(nameof(shopClient));
    }

    private bool IsTokenChecked { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if(!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await _localStorageService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                Shopclient.SetAuthorizationToken(token);
            }
        }
    }
}