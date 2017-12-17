using System.Threading.Tasks;
using TrackYourLife_IoT.Business.Models;

namespace TrackYourLife_IoT.Business.Services
{
    public interface ISensorsService
    {
        Task<SensorMeasurmentResult> GetCurrentSensorMeasurment();
    }
}
