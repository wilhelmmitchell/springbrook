using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;

using WeatherApp.Data.Models;
using WeatherApp.Data.Contracts;

namespace WeatherApp.Data.Services
{
    public class ZoneService : IZoneService
    {
        private readonly HttpClient _httpClient;

        public ZoneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Zone> GetZone(string zonecode)
        {
            Zone retval = null;

            try
            {
                //get serialized zone information from weather API
                var jsonStream = await _httpClient.GetStreamAsync($"/zones/public?id={zonecode}");

                //manually pluck name of zone from json response
                var zoneResponse = JsonNode.Parse(jsonStream);
                JsonArray features = zoneResponse!["features"]!.AsArray();
                if (features.Any())
                {
                    string zoneName = features.FirstOrDefault()!["properties"]!["name"]!.GetValue<string>();
                    retval = new Zone { Name = zoneName };
                }
            }
            catch (HttpRequestException ex)
            {
                //app will receive 400 bad request if user types in an invalid zone
            }

            return retval;
        }
    }
}
