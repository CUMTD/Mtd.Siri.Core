using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class MonitoredCall
{
	public int? VisitNumber { get; set; }

	[XmlElement("VehicleAtStop")]
	public bool? IsVehicleAtStop { get; set; }

	[XmlElement("DestinationDisplay")]
	public string? Headsign { get; set; }

	[XmlElement("AimedArrivalTime")]
	public DateTime? ScheduledArrival { get; set; }

	[XmlElement("ExpectedArrivalTime")]
	public DateTime? EstimatedArrival { get; set; }

	[XmlElement("AimedDepartureTime")]
	public DateTime? ScheduledDeparture { get; set; }

	[XmlElement("ExpectedDepartureTime")]
	public DateTime? EstimatedDeparture { get; set; }

	[XmlElement("DeparturePlatformName")]
	public string? DeparturePlatform { get; set; }

	[XmlIgnore]
	public TimeSpan? ScheduleAdherence =>
		(EstimatedDeparture.HasValue && ScheduledDeparture.HasValue)
			? EstimatedDeparture.Value - ScheduledDeparture.Value
			: (TimeSpan?)null;
}
