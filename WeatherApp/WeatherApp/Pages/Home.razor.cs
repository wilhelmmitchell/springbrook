using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using WeatherApp.Data.Contracts;
using WeatherApp.Data.Services;
using WeatherApp.Data.Models;
using WeatherApp.Components;

namespace WeatherApp.Pages
{
    public partial class Home
    {
        private string CurrentZoneCode;
        private string CurrentZoneName;
        
        public List<Forecast> Forecasts { get; set; } = new List<Forecast>();

        [Inject]
        private IForecastService _forecastService { get; set; } = default!;
        [Inject]
        private IZoneService _zoneService { get; set; } = default!;

        protected override void OnInitialized()
        {
        }

        private async Task RefreshForecast() 
        {
            CurrentZoneName = (await _zoneService.GetZone(CurrentZoneCode))?.Name ?? "Unknown Zone";
            Forecasts = (await _forecastService.GetForecastForZone(CurrentZoneCode)).ToList();
        }

        private async Task OnChange_Zone(ChangeEventArgs args)
        {
            CurrentZoneCode = args.Value.ToString();
            await RefreshForecast();
        }
        private async Task OnClick_Refresh()
        {
            await RefreshForecast();
        }
    }
}
