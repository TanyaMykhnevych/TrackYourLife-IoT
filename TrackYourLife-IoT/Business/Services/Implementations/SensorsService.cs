using Sensors.Dht;
using System;
using System.Threading.Tasks;
using TrackYourLife_IoT.Business.Models;
using TrackYourLife_IoT.Data.Sensors.Interfaces;
using TrackYourLife_IoT.Data.Sensors.Models;

namespace TrackYourLife_IoT.Business.Services.Implementations
{
    public class SensorsService : ISensorsService
    {
        private readonly ISensorsReader<DhtReading> _dhtReader;

        public SensorsService(ISensorsReader<DhtReading> dhtReader)
        {
            _dhtReader = dhtReader;
        }
        
        public async Task<SensorMeasurmentResult> GetCurrentSensorMeasurment()
        {
            SensorReadingWrapper<DhtReading> reading;
            SensorMeasurmentResult result = new SensorMeasurmentResult();

            int counter = 0;
            do
            {
                reading = await _dhtReader.ReadAsync();
                await Task.Delay(50);
            } while (!IsReadingValid(reading) && counter++ < 5);

            if (!IsReadingValid(reading))
            {
                throw new SensorReadingException();
            }

            result.Humidity = (float)reading.Data.Humidity;
            result.Temperature = (float)reading.Data.Temperature;

            return result;
        }

        private bool IsReadingValid<T>(SensorReadingWrapper<T> data)
        {
            if (!data.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
