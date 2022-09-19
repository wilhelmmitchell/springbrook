using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherApp;
using WeatherApp.Data.Contracts;
using WeatherApp.Data.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string WEATHER_API_URI = builder.Configuration["WEATHER_API_URI"];

//setup http client factory so that app can more easily be converted between client and service side blazor 
builder.Services.AddHttpClient<IForecastService, ForecastService>(client => client.BaseAddress = new Uri(WEATHER_API_URI));
builder.Services.AddHttpClient<IZoneService, ZoneService>(client => client.BaseAddress = new Uri(WEATHER_API_URI));

await builder.Build().RunAsync();
