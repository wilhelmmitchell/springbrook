using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using WeatherApp.Data.Models;

namespace WeatherApp.Components
{
    public partial class ForecastComponent
    {
        [Parameter]
        public Forecast Forecast { get; set; }
    }
}