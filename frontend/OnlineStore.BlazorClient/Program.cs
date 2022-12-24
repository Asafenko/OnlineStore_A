using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineStore.BlazorClient;
using MudBlazor.Services;
using OnlineStore.BlazorClient.BasketShop;
using OnlineStore.HttpApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

// Blazored Toast
builder.Services.AddBlazoredToast();


// HttpApiClientRefit
// builder.AddRefitClient<IShopClientRefit>()
//     .ConfigurenHttpClient(HttpClient => HttpClient.BaseAddres = new Uri("https://localhost:7079"));


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
// HttpApiClient
builder.Services.AddSingleton<IShopClient>(new ShopClient("https://localhost:7079"));
// HttpApiClientFake
//builder.Services.AddSingleton<IShopClient>(new ShopClientFake(TimeSpan.FromSeconds(1)));


// FOR BASKET
builder.Services.AddSingleton<ICartService,CartService>();

await builder.Build().RunAsync();
