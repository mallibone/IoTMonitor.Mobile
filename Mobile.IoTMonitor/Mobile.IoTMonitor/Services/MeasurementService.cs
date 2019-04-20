using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mobile.IoTMonitor
{
    class MeasurementService : IMeasurementService
    {
        private HubConnection _connection;
        private const string backendUrl = "https://iotdemofunction.azurewebsites.net/api/";
        //private const string backendUrl = "http://localhost:7071/api/";
        public event EventHandler<Measurement> NewMeasurement;

        public MeasurementService()
        {
            _connection = new HubConnectionBuilder()
                //.WithUrl("https://iotdemofunction.azurewebsites.net/api/IoTPlayground")
                .WithUrl(backendUrl)
                .Build();
        }

        public async Task Connect()
        {
            if (_connection.State == HubConnectionState.Connected) return;

            _connection.On<string>("measurement", (messageString) =>
            {
                var message = JsonConvert.DeserializeObject<Measurement>(messageString);
                NewMeasurement?.Invoke(this, message);
                Debug.WriteLine(messageString);
            });

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task Disconnect()
        {
            await _connection.DisposeAsync();

            _connection = new HubConnectionBuilder()
                //.WithUrl("https://iotdemofunction.azurewebsites.net/api/IoTPlayground")
                //.WithUrl("https://iotdemofunction.azurewebsites.net/api/negotiate")
                .WithUrl(backendUrl)
                .Build();
        }
    }
}
