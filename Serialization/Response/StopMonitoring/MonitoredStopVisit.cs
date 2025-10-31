using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class MonitoredStopVisit
	{
		[XmlElement("RecordedAtTime")]
		public DateTime RecordedAt { get; set; }

		[XmlElement("MonitoringRef")]
		public string StopId { get; set; }

		public MonitoredVehicleJourney MonitoredVehicleJourney { get; set; }
	}
}