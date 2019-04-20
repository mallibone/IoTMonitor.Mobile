using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.IoTMonitor
{
    interface IMeasurementService
    {
        event EventHandler<Measurement> NewMeasurement;

        Task Connect();
        Task Disconnect();
    }
}