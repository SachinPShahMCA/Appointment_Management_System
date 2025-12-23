using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Observability.Stores
{
    public interface IPerformanceStore
    {
        Task StoreAsync(HttpContext context, long elapsedMs, int thresholdMs);
    }
}
