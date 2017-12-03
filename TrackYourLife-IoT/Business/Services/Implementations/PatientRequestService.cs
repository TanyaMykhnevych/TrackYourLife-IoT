using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Api.APIs;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;

namespace TrackYourLife_IoT.Business.Services.Implementations
{
    internal class PatientRequestService : IPatientRequestService
    {
        private readonly IPatientRequestRestApi _requestRestApi;

        public PatientRequestService(IPatientRequestRestApi requestRestApi)
        {
            _requestRestApi = requestRestApi;
        }

        public async Task<ResponseWrapper<PatientRequestListModel>> GetReadyToTransportPatientRequestsAsync()
        {
            return await _requestRestApi.GetReadyToTransportPatientRequestsAsync();
        }
    }
}
