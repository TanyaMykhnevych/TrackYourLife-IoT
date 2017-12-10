using System;

namespace TrackYourLife_IoT.Business
{
    public class SensorReadingException : InvalidOperationException
    {
        public SensorReadingException()
        {

        }

        public SensorReadingException(string message)
            : base(message)
        {

        }
    }
}
