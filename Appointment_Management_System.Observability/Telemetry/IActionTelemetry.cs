using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Telemetry
{
    public interface IActionTelemetry
    {
        void Track(string action, object data = null);
    }

}
