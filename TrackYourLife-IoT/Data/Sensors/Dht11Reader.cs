using Sensors.Dht;
using System;
using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Sensors.Interfaces;
using TrackYourLife_IoT.Data.Sensors.Models;
using Windows.Devices.Gpio;

namespace TrackYourLife_IoT.Drivers.DHT11
{
    public class Dht11Reader : ISensorsReader<DhtReading>
    {
        public async Task<SensorReadingWrapper<DhtReading>> ReadAsync()
        {
            var readingData = new SensorReadingWrapper<DhtReading>(d => d.IsValid);

            var controller = GpioController.GetDefault();
            using (GpioPin pin = controller.OpenPin(4, GpioSharingMode.Exclusive))
            {
                using (Dht11 dht11 = new Dht11(pin, GpioPinDriveMode.Input))
                {
                    readingData.Data = await dht11.GetReadingAsync().AsTask();
                }
            }

            return readingData;
        }
    }
}
