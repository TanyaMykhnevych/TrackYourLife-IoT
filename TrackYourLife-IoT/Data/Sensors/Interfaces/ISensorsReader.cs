using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Sensors.Models;

namespace TrackYourLife_IoT.Data.Sensors.Interfaces
{
    public interface ISensorsReader<T>
    {
        Task<SensorReadingWrapper<T>> ReadAsync();
    }
}
