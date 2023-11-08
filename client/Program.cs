using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using auc_client;
using auc_client.Shared;
using auc_client.ViewModels;
using Blazored.LocalStorage;
using Blazored.Toast;
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