namespace TrackYourLife_IoT.Business.Services
{
    public interface INetworkService
    {
        bool IsInternetConnectionAvailable { get; }
    }
}
