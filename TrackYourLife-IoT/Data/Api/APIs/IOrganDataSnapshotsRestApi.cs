using System.Threading.Tasks;
using UwpClientApp.Presentation.Models.OrganDataSnapshots;

namespace TrackYourLife_IoT.Data.Api.APIs
{
    public interface IOrganDataSnapshotsRestApi
    {
        Task SendOrganDataSnapshotAsync(OrganStateSnapshotModel model);
    }
}
