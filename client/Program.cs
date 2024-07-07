using Blazored.LocalStorage;
using Blazored.Toast;
using Client;
using Client.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var currentAssembly = typeof(Program).Assembly;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
        new HttpClient { BaseAddress = new Uri(EnvUrls.BaseUrl) })
    .AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>()
    .AddAuthorizationCore()
    .AddScoped<GenericMethods>()
    .AddBlazoredLocalStorage()
    .AddBlazoredToast()
    .AddFluxor(options => options.ScanAssemblies(currentAssembly))
    //ViewModels
    /*.AddScoped<IGarageViewModel, GarageViewModel>()
    .AddScoped<IAddItemViewModel, AddItemViewModel>()*/
    ;

await builder.Build().RunAsync();