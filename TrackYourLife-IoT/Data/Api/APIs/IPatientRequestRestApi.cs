using System.Threading.Tasks;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;

namespace TrackYourLife_IoT.Data.Api.APIs
{
    public interface IPatientRequestRestApi
    {
        Task<ResponseWrapper<PatientRequestListModel>> GetReadyToTransportPatientRequestsAsync();
    }
}
