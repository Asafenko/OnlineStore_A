using System.ComponentModel;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineStore.HttpApiClient;

namespace OnlineStore.BlazorClient.Pages;

public class AppComponentBase : ComponentBase
{
    private readonly IShopClient _client;
    private readonly ILocalStorageService _localStoregeService;


    public AppComponentBase(IShopClient shopClient,ILocalStorageService localStorageService)
    {
        _localStoregeService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        _client = shopClient ?? throw new ArgumentNullException(nameof(shopClient));
    }
    
    protected bool IsTokenChecked { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if(!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await _localStoregeService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                //_client;
            }
        }
    }
}