using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace Mobile.IoTMonitor.ViewModels
{
    internal class MainViewModel : ReactiveObject, IDisposable
    {
        private string _temperature;
        private string _humidity;
        private string _pressure;
        private IMeasurementService _measurementService;
        private IDisposable _measurementSubscription;

        public MainViewModel(IMeasurementService measurementService = null)
        {
            _measurementService = measurementService ?? new MeasurementService();
        }

        public async Task Init()
        {
            await _measurementService.Connect();
            _measurementSubscription = _measurementService.NewMeasurement.Subscribe(msg => {
                Temperature = msg.Temperature.ToString();
                Pressure = msg.Pressure.ToString();
                Humidity = msg.Humidity.ToString();
            });
        }

        public string Temperature 
        {
            get => _temperature; 
            set => this.RaiseAndSetIfChanged(ref _temperature, value + "° C"); 
        }

        public string Pressure
        {
            get => _pressure;
            set => this.RaiseAndSetIfChanged(ref _pressure, value + " psig");
        }

        public string Humidity
        {
            get => _humidity;
            set => this.RaiseAndSetIfChanged(ref _humidity, value + " %");
        }

        public async void Dispose()
        {
            _measurementSubscription.Dispose();
            await _measurementService.Disconnect();
        }
    }
}
