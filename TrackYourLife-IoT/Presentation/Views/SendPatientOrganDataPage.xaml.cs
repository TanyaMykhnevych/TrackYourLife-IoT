using Autofac;
using ReactiveUI;
using System;
using TrackYourLife_IoT.Presentation.ViewModels.DonorRequest;
using Windows.UI.Xaml;


namespace TrackYourLife_IoT.Presentation.Views
{
    public sealed partial class SendPatientOrganDataPage : IViewFor<SendPatientOrganDataViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(SendPatientOrganDataViewModel),
                typeof(SendPatientOrganDataPage),
                new PropertyMetadata(default(SendPatientOrganDataViewModel)));

        public SendPatientOrganDataPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<SendPatientOrganDataViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.BindCommand(ViewModel, vm => vm.StartSendingCommand, v => v.StartButton));
            d(this.BindCommand(ViewModel, vm => vm.StopSendingCommand, v => v.StopButton));
            d(this.BindCommand(ViewModel, vm => vm.RefreshPatientRequestList, v => v.RefreshButton));

            d(this.OneWayBind(ViewModel, vm => vm.DataSendingIsInProgress, v => v.StartButton.Visibility,
                isInProgress => isInProgress ? Visibility.Collapsed : Visibility.Visible));
            d(this.OneWayBind(ViewModel, vm => vm.DataSendingIsInProgress, v => v.StopButton.Visibility,
            isInProgress => isInProgress ? Visibility.Visible : Visibility.Collapsed));

            d(this.OneWayBind(ViewModel, vm => vm.PatientRequestList, v => v.PatientRequestComboBox.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedPatientRequest, v => v.PatientRequestComboBox.SelectedItem));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SendPatientOrganDataViewModel)value;
        }

        public SendPatientOrganDataViewModel ViewModel
        {
            get => (SendPatientOrganDataViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
