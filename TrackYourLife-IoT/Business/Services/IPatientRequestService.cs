using System.Threading.Tasks;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;

namespace TrackYourLife_IoT.Business.Services
{
    public interface IPatientRequestService
    {
        Task<ResponseWrapper<PatientRequestListModel>> GetReadyToTransportPatientRequestsAsync();
    }
}
