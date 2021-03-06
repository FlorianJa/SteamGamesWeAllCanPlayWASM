using SteamGamesWeAllCanPlayWASM.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SteamGamesWeAllCanPlayWASM.Client;
using SteamGamesWeAllCanPlayWASM.Client.Services;
using SteamGamesWeAllCanPlay;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, SteamAuthProvider>();
builder.Services.AddScoped<SteamService, SteamService>();
builder.Services.AddSingleton<AppState>();
await builder.Build().RunAsync();
