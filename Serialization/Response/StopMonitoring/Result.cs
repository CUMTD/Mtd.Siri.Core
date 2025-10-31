using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring
{
	[Serializable]
	[XmlType("StopMonitoringDelivery", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class Result
	{
		[XmlElement("ResponseTimestamp")]
		public DateTime Timestamp { get; set; }

		public bool Status { get; set; }

		[XmlElement("MonitoredStopVisit")]
		public MonitoredStopVisit[]? Results { get; set; }

		[XmlAttribute("version")]
		public decimal Version { get; set; }
	}
}
