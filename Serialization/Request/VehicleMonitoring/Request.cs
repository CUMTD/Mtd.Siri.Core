using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.VehicleMonitoring
{
	[Serializable]
	[XmlType("VehicleMonitoringRequest", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class Request
	{
		[XmlElement("RequestTimestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		public Request()
		{
			Timestamp = TimeProvider.System.GetLocalNow();
			Version = "1.0";
		}
	}
}
