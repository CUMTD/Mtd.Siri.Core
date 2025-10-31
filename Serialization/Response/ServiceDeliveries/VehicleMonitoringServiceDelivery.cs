using System.Xml.Serialization;
using Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries
{
	public class VehicleMonitoringServiceDelivery : ServiceDelivery
	{
		[XmlElement("VehicleMonitoringDelivery")]
		public VehicleMonitoringDelivery? Delivery { get; set; }
	}
}
