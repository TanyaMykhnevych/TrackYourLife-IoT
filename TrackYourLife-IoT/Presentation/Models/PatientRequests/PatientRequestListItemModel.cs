using UwpClientApp.Presentation.Enums;

namespace TrackYourLife_IoT.Presentation.Models.PatientRequests
{
    public class PatientRequestListItemModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Message { get; set; }

        public PatientRequestStatuses Status { get; set; }

        public int? PatientInfoId { get; set; }

        public int OrganInfoId { get; set; }
        public string OrganInfoName { get; set; }
        
        public bool HasLinkedDonorRequest { get; set; }
    }
}
