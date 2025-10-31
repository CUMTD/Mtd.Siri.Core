using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;

[Serializable]
[XmlType("MonitoredCall", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class MonitoredCall
{
	public int? VisitNumber { get; set; }

	[XmlElement("StopPointName")]
	public string? StopName { get; set; }

	[XmlElement("VehicleAtStop")]
	public bool? VehicleIsAtStop { get; set; }

	[XmlElement("DestinationDisplay")]
	public string? Headsign { get; set; }

	[XmlElement("AimedArrivalTime")]
	public DateTime? ScheduledArrival { get; set; }

	[XmlElement("ExpectedArrivalTime")]
	public DateTime? ExpectedArrival { get; set; }

	[XmlElement("AimedDepartureTime")]
	public DateTime? ScheduledDeparture { get; set; }

	[XmlElement("ExpectedDepartureTime")]
	public DateTime? ExpectedDeparture { get; set; }
}
