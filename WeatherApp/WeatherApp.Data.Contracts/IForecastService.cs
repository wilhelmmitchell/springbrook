using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WeatherApp.Data.Models;

namespace WeatherApp.Data.Contracts
{
    public interface IForecastService
    {
        Task<IEnumerable<Forecast>> GetForecastForZone(string zonecode);
    }
}
