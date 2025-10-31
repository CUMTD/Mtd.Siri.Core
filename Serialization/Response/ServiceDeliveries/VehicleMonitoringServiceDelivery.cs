using Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;

public class VehicleMonitoringServiceDelivery : ServiceDelivery
{
	[XmlElement("VehicleMonitoringDelivery")]
	public VehicleMonitoringDelivery? Delivery { get; set; }
}
