using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UmbracoDemo.Client;
using UmbracoDemo.Client.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddTransient<IUmbracoClient, UmbracoClient>()
    .AddHttpClient<IUmbracoApi, UmbracoApi>(
    (client, sp) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        return new UmbracoApi(config["UmbracoApi:BaseUrl"], client);
    });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
