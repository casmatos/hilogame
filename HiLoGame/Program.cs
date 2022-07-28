using HiLoGame;
using HiLoGame.Model;
using HiLoGame.Network.Http;
using HiLoGame.Services;
using HiLoGame.Services.Session;
using HiLoGame.Shared.DTO;
using HiLoGame.Shared.DTO.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<PlayerRequestClient>(client => client.BaseAddress = new Uri("https://localhost:7228/"));
builder.Services.AddHttpClient<RoomRequestClient>(client => client.BaseAddress = new Uri("https://localhost:7228/"));

builder.Services.AddScoped<SessionGaming>();

builder.Services.AddTransient<IServiceModelClient<ResponseModel<PlayerDTO>, GamePlayer>, PlayerService>();
builder.Services.AddTransient<IServiceModelClient<ResponseModel<RoomDTO>, GameRoom>, RoomService>();

builder.Services.AddTransient<MisteryService>();

builder.Services.AddMudServices();

await builder
    .Build()
    .RunAsync();
