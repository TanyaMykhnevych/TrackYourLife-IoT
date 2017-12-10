using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackYourLife_IoT.Business.Services
{
    public interface ISensorsService
    {
        Task<float> GetCurrentTemperatureAsync();

        Task<float> GetCurrentHumidityAsync();
    }
}
