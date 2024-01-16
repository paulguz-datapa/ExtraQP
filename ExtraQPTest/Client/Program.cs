using ExtraQPTest.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{


  // Configure your authentication provider options here.
  // For more information, see https://aka.ms/blazor-standalone-auth
  //builder.Configuration.Bind("Local", options.ProviderOptions);
  options.ProviderOptions.Authority = "*****";
  options.ProviderOptions.ClientId = $"******";
  options.ProviderOptions.ResponseType = "code";
  options.ProviderOptions.ResponseMode = "query";
  options.UserOptions.RoleClaim = "role";
  options.ProviderOptions.PostLogoutRedirectUri = builder.HostEnvironment.BaseAddress;

});

builder.Services.AddHttpClient("ExtraQPTest.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ExtraQPTest.ServerAPI"));

await builder.Build().RunAsync();
