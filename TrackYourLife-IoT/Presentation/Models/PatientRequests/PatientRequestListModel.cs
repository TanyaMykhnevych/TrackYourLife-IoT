using System.Collections.Generic;

namespace TrackYourLife_IoT.Presentation.Models.PatientRequests
{
    public class PatientRequestListModel
    {
        public ICollection<PatientRequestListItemModel> PatientRequestList { get; set; }
    }
}
