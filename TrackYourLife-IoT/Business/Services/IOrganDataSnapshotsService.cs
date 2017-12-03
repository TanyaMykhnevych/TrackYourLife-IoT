using System.Threading.Tasks;
using TrackYourLife_IoT.Presentation.Models;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;
using UwpClientApp.Presentation.Models.OrganDataSnapshots;

namespace TrackYourLife_IoT.Business.Services
{
    public interface IOrganDataSnapshotsService
    {
        Task SendOrganDataSnapshotAsync(OrganStateSnapshotModel model);
    }
}
