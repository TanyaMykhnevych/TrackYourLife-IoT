using Autofac;
using TrackYourLife_IoT.Business.Services;
using TrackYourLife_IoT.Business.Services.Implementations;
using TrackYourLife_IoT.Data.Api.APIs;
using TrackYourLife_IoT.Data.Api.APIs.Implementations;
using TrackYourLife_IoT.Presentation.ViewModels.DonorRequest;

namespace TrackYourLife_IoT.Infrastructure
{
    public static class AutofacRegistrator
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            RegisterApis(builder);
            RegisterServices(builder);
            RegisterViewModels(builder);
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<SendPatientOrganDataViewModel>().AsSelf().AsImplementedInterfaces();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PreferencesService>().As<IPreferencesService>();
            builder.RegisterType<NetworkService>().As<INetworkService>();
            builder.RegisterType<PatientRequestService>().As<IPatientRequestService>();
            builder.RegisterType<OrganDataSnapshotsService>().As<IOrganDataSnapshotsService>();
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<PatientRequestRestApi>().As<IPatientRequestRestApi>();
            builder.RegisterType<OrganDataSnapshotsRestApi>().As<IOrganDataSnapshotsRestApi>();
        }
    }
}
