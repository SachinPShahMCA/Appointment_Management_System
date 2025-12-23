using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Stores
{
    public interface ITelemetryStore
    {
        Task StoreAsync(string eventName, object? payload);
    }
}
