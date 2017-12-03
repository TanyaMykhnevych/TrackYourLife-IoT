using System.Threading.Tasks;
using TrackYourLife_IoT.Data.Api.APIs;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;
using UwpClientApp.Presentation.Models.OrganDataSnapshots;

namespace TrackYourLife_IoT.Business.Services.Implementations
{
    internal class OrganDataSnapshotsService : IOrganDataSnapshotsService
    {
        private readonly IOrganDataSnapshotsRestApi _requestRestApi;

        public OrganDataSnapshotsService(IOrganDataSnapshotsRestApi requestRestApi)
        {
            _requestRestApi = requestRestApi;
        }

        public Task SendOrganDataSnapshotAsync(OrganStateSnapshotModel model)
        {
            return _requestRestApi.SendOrganDataSnapshotAsync(model);
        }
    }
}
