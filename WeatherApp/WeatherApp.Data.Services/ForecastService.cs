using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;

using WeatherApp.Data.Models;
using WeatherApp.Data.Contracts;

namespace WeatherApp.Data.Services
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient _httpClient;

        public ForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Forecast>> GetForecastForZone(string zonecode) 
        {
            var retval = new List<Forecast>();

            try
            {
                //get serialized forecast information from weather API
                var jsonStream = await _httpClient.GetStreamAsync($"/zones/public/{zonecode}/forecast");

                //get a subsection of the json response and deserialize it into forecast objects
                var forecastResponse = JsonNode.Parse(jsonStream);
                var periods = forecastResponse!["properties"]!["periods"]!.AsArray();
                var serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                foreach (var period in periods) retval.Add(period.Deserialize<Forecast>(serializerOptions)!);
            }
            catch (HttpRequestException ex)
            {
                //app will receive 400 bad request if user types in an invalid zone
            }

            return retval;
        }
    }
}
