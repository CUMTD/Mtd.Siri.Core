using Mtd.Siri.Core.Client.Generic;
using Mtd.Siri.Core.Serialization.Request.VehicleMonitoring;
using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client;
public interface IVehicleMonitoringClient : ISubscriptionClient<VMSubscriptionRequest, MonitoredVehicle>
{
}
