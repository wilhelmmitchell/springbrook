using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using WeatherApp.Data.Contracts;
using WeatherApp.Data.Services;

namespace WeatherApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            string WEATHER_API_URI = builder.Configuration["WEATHER_API_URI"];

            //setup http client factory so that app can more easily be converted between client and service side blazor 
            builder.Services.AddHttpClient<IForecastService, ForecastService>(client => client.BaseAddress = new Uri(WEATHER_API_URI));
            builder.Services.AddHttpClient<IZoneService, ZoneService>(client => client.BaseAddress = new Uri(WEATHER_API_URI));

            await builder.Build().RunAsync();
        }
    }
}
