using System;

namespace TrackYourLife_IoT.Data.Sensors.Models
{
    public class SensorReadingWrapper<T>
    {
        private readonly Func<T, bool> _isValidFunc;

        public SensorReadingWrapper(Func<T, bool> isValidFunc)
        {
            _isValidFunc = isValidFunc;
        }

        public T Data { get; set; }

        public bool IsValid => 
            Data != null && _isValidFunc(Data);
    }
}
