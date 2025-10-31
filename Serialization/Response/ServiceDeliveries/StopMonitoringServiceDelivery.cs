using Mtd.Siri.Core.Serialization.Response.StopMonitoring;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries
{
	public class StopMonitoringServiceDelivery : RequestResponseServiceDelivery
	{
		[XmlElement("StopMonitoringDelivery")]
		public Result[]? Result { get; set; }
	}
}
