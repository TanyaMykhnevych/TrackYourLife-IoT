using System;
using System.Globalization;
using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Api.Rest;
using TrackYourLife_IoT.Presentation.Models;
using UwpClientApp.Presentation.Models.OrganDataSnapshots;

namespace TrackYourLife_IoT.Data.Api.APIs.Implementations
{
    public class OrganDataSnapshotsRestApi : RestApiBase, IOrganDataSnapshotsRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "OrganDelivery";

        public OrganDataSnapshotsRestApi() : base(new Uri(BaseApiAddress))
        {
        }

        public Task SendOrganDataSnapshotAsync(OrganStateSnapshotModel model)
        {
            return Url($"{ControllerPath}/AttachOrganDeliverySnapshot")
                .FormUrlEncoded()
                .Param(nameof(model.PatientRequestId), model.PatientRequestId.ToString())
                .Param(nameof(model.Altitude), model.Altitude.ToString())
                .Param(nameof(model.Longitude), model.Longitude.ToString())
                .Param(nameof(model.Temperature), model.Temperature.ToString(CultureInfo.InvariantCulture))
                .Param(nameof(model.Time), model.Time.ToString())
                .PostAsync<ResponseWrapper>();
        }
    }
}
