using Sensors.Dht;
using System;
using System.Threading.Tasks;
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

        public async Task<float> GetCurrentHumidityAsync()
        {
            var reading = await _dhtReader.ReadAsync();

            if (!IsReadingValid(reading))
            {
                throw new SensorReadingException();
            }

            return (float)reading.Data.Humidity;
        }

        public async Task<float> GetCurrentTemperatureAsync()
        {
            SensorReadingWrapper<DhtReading> reading;

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

            return (float)reading.Data.Temperature;
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
