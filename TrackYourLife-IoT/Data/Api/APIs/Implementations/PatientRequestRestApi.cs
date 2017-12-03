using System;
using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Api.Rest;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;

namespace TrackYourLife_IoT.Data.Api.APIs.Implementations
{
    public class PatientRequestRestApi : RestApiBase, IPatientRequestRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;

        public PatientRequestRestApi() : base(new Uri(BaseApiAddress))
        {
        }

        public async Task<ResponseWrapper<PatientRequestListModel>> GetReadyToTransportPatientRequestsAsync()
        {
             var response = await Url("patientRequest/getReadyToTransportPatientRequests")
                    .GetAsync<ResponseWrapper<PatientRequestListModel>>();
            return response;
        }
    }
}
