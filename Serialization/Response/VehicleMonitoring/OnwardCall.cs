using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType("OnwardCall", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class OnwardCall
	{
		public byte VisitNumber { get; set; }

		[XmlElement("StopPointName")]
		public string StopName { get; set; } = string.Empty!;

		[XmlElement("StopPointRef")]
		public string StopId { get; set; } = string.Empty!;

		[XmlElement("AimedArrivalTime")]
		public DateTime ScheduledArrival { get; set; }

		[XmlElement("ExpectedArrivalTime")]
		public DateTime ExpectedArrival { get; set; }

		[XmlElement("AimedDepartureTime")]
		public DateTime ScheduledDeparture { get; set; }

		[XmlElement("ExpectedDepartureTime")]
		public DateTime ExpectedDeparture { get; set; }
	}
}
