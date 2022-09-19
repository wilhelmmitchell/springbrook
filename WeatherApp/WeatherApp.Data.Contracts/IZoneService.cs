using System;
using System.Threading.Tasks;

using WeatherApp.Data.Models;

namespace WeatherApp.Data.Contracts
{
    public interface IZoneService
    {
        Task<Zone?> GetZone(string zonecode);
    }
}
