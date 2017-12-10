using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrackYourLife_IoT.Business;
using TrackYourLife_IoT.Business.Services;
using TrackYourLife_IoT.Presentation.Models.PatientRequests;
using UwpClientApp.Presentation.Enums;
using UwpClientApp.Presentation.Models.OrganDataSnapshots;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace TrackYourLife_IoT.Presentation.ViewModels.DonorRequest
{
    public class SendPatientOrganDataViewModel : ViewModelBase
    {
        private const int SendingPeriodInSeconds = 10;

        private readonly IPatientRequestService _patientRequestService;
        private readonly IOrganDataSnapshotsService _organDataSnapshotsService;
        private readonly ISensorsService _sensorsService;


        private Timer _sendingOrganDataTimer;
        private CoreDispatcher _dispatcher;


        private ReactiveList<PatientRequestListItemModel> _patientRequestList;
        private PatientRequestListItemModel _selectedPatientRequest;
        private bool _dataSendingIsInProgress;

        public SendPatientOrganDataViewModel(
            IPatientRequestService patientRequestService,
            IOrganDataSnapshotsService organDataSnapshotsService,
            ISensorsService sensorsService)
        {
            _patientRequestService = patientRequestService;
            _organDataSnapshotsService = organDataSnapshotsService;
            _sensorsService = sensorsService;

            Init();
        }

        public ReactiveList<PatientRequestListItemModel> PatientRequestList
        {
            get => _patientRequestList;
            set => this.RaiseAndSetIfChanged(ref _patientRequestList, value);
        }

        public PatientRequestListItemModel SelectedPatientRequest
        {
            get => _selectedPatientRequest;
            set => this.RaiseAndSetIfChanged(ref _selectedPatientRequest, value);
        }

        public bool DataSendingIsInProgress
        {
            get => _dataSendingIsInProgress;
            set => this.RaiseAndSetIfChanged(ref _dataSendingIsInProgress, value);
        }

        public ReactiveCommand StartSendingCommand { get; set; }

        public ReactiveCommand StopSendingCommand { get; set; }

        private async void Init()
        {
            StartSendingCommand = ReactiveCommand.CreateFromTask(StartSendingCommandExecuted);
            StopSendingCommand = ReactiveCommand.CreateFromTask(StopSendingCommandExecuted);

            this.ObservableForProperty(x => x.DataSendingIsInProgress).Subscribe(args =>
            {
                _sendingOrganDataTimer?.Dispose();

                if (args.Value)
                {
                    var startTimeSpan = TimeSpan.Zero;
                    var periodTimeSpan = TimeSpan.FromSeconds(SendingPeriodInSeconds);

                    _dispatcher = Window.Current.Dispatcher;
                    _sendingOrganDataTimer = new Timer(async (e) =>
                    {
                        await SendMeasurementData();
                    }, null, startTimeSpan, periodTimeSpan);
                }
            });

            PatientRequestList = new ReactiveList<PatientRequestListItemModel>(
                await LoadPatientRequestListAsync());
        }

        private async Task SendMeasurementData()
        {
            if (IsBusy) return;

            await _dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            {
                OnIsInProgressChanges(true);

                var temperature = await GetTemperatureAsync();
                if (!IsTemperatureWasReadSuccessfully(temperature))
                {
                    return;
                }

                var random = new Random();
                var model = new OrganStateSnapshotModel
                {
                    PatientRequestId = SelectedPatientRequest.Id,
                    Altitude = random.NextDouble(),
                    Longitude = random.NextDouble(),
                    Temperature = temperature,
                    Time = DateTime.UtcNow
                };

                await _organDataSnapshotsService.SendOrganDataSnapshotAsync(model);
                await Task.Delay(1000);

                OnIsInProgressChanges(false);
            });
        }

        private async Task<float> GetTemperatureAsync()
        {
            float temp = float.MinValue;

            try
            {
                temp = await _sensorsService.GetCurrentTemperatureAsync();
            }
            catch (SensorReadingException)
            {
                await ShowErrorAsync("Cannot read sensor data");
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }

            return temp;
        }

        private bool IsTemperatureWasReadSuccessfully(float value)
        {
            return value != float.MinValue;
        } 

        private async Task StartSendingCommandExecuted()
        {
            if (await Validate())
            {
                DataSendingIsInProgress = true;
            }
        }

        private async Task StopSendingCommandExecuted()
        {
            DataSendingIsInProgress = false;
        }

        private async Task<bool> Validate()
        {
            if (SelectedPatientRequest == null
                || SelectedPatientRequest.Status != PatientRequestStatuses.AwaitingForTransplanting)
            {
                await ShowErrorAsync("Firstly, chose correct patient request.");
                return false;
            }

            return true;
        }

        private async Task<List<PatientRequestListItemModel>> LoadPatientRequestListAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var patientRequestListResponse = await _patientRequestService.GetReadyToTransportPatientRequestsAsync();

                if (!patientRequestListResponse.IsValid)
                {
                    await ShowErrorAsync(string.IsNullOrEmpty(patientRequestListResponse.ErrorMessage)
                        ? "Load Patient Request Failed."
                        : patientRequestListResponse.ErrorMessage);
                    return null;
                }

                return patientRequestListResponse.Content.PatientRequestList.ToList();
            }
            catch(Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }

            return Enumerable.Empty<PatientRequestListItemModel>().ToList();
        }
    }
}
