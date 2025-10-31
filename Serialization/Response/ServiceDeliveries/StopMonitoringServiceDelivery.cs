using System.Xml.Serialization;
using Mtd.Siri.Core.Serialization.Response.StopMonitoring;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries
{
	public class StopMonitoringServiceDelivery : RequestResponseServiceDelivery
	{
		[XmlElement("StopMonitoringDelivery")]
		public Result[]? Result { get; set; }
	}
}
