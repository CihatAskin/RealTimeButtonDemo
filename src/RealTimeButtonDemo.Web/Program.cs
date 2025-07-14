using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealTimeButtonDemo.Web;
using RealTimeButtonDemo.Web.Services;
using RealTimeButtonDemo.Shared.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to point to our API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(ApiConstants.DefaultBaseUrl)
});

// Add authentication service
builder.Services.AddScoped<AuthenticationService>();

await builder.Build().RunAsync();
