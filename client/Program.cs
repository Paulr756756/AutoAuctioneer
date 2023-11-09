using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Shared;
using Client.ViewModels;
using Blazored.LocalStorage;
using Blazored.Toast;
using Client;
using Microsoft.AspNetCore.Components.Authorization;
using Fluxor;


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