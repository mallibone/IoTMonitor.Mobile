using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.IoTMonitor
{
    public partial class MainPage : ContentPage
    {
        private HubConnection _connection;
        private MeasurementService _measurementService = new MeasurementService();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _measurementService.Connect();

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _measurementService.Disconnect();
        }
    }
}
