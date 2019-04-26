﻿using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace Mobile.IoTMonitor.ViewModels
{
    internal class MainViewModel : ReactiveObject, IDisposable
    {
        private string _temperatureLabel = "Temperature";
        private float _temperature = 1f;
        private string _humidtyLabel = "Humidity";
        private float _humidity = 1f;
        private string _pressureLabel = "Pressure";
        private float _pressure = 1f;
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
                TemperatureLabel = msg.TemperatureUnit;
                Temperature = msg.Temperature;
                PressureLabel = msg.PressureUnit;
                Pressure = msg.Pressure;
                HumidityLabel = msg.HumidityUnit;
                Humidity = msg.Humidity;
            });
        }

        public string TemperatureLabel 
        {
            get => _temperatureLabel; 
            set => this.RaiseAndSetIfChanged(ref _temperatureLabel, value); 
        }
        public float Temperature 
        {
            get => _temperature; 
            set => this.RaiseAndSetIfChanged(ref _temperature, value); 
        }

        public string PressureLabel 
        {
            get => _pressureLabel; 
            set => this.RaiseAndSetIfChanged(ref _pressureLabel, value); 
        }
        public float Pressure
        {
            get => _pressure;
            set => this.RaiseAndSetIfChanged(ref _pressure, value);
        }

        public string HumidityLabel 
        {
            get => _humidtyLabel; 
            set => this.RaiseAndSetIfChanged(ref _humidtyLabel, value); 
        }
        public float Humidity
        {
            get => _humidity;
            set => this.RaiseAndSetIfChanged(ref _humidity, value);
        }

        public async void Dispose()
        {
            _measurementSubscription.Dispose();
            await _measurementService.Disconnect();
        }
    }
}
