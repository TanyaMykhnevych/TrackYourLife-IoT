using System;

namespace UwpClientApp.Presentation.Models.OrganDataSnapshots
{
    public class OrganStateSnapshotModel
    {
        public int PatientRequestId { get; set; }

        public double Altitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Time { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }
    }
}
