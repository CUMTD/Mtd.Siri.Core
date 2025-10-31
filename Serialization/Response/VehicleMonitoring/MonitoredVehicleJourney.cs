using Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;
using System.Xml.Serialization;

[Serializable]
[XmlType("MonitoredVehicleJourney", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class MonitoredVehicleJourney
{
	[XmlElement("LineRef")]
	public string? RouteId { get; set; }

	[XmlElement("DirectionRef")]
	public string? Direction { get; set; }

	public FramedVehicleJourneyRef? FramedVehicleJourneyRef { get; set; }

	[XmlElement("JourneyPatternRef")]
	public string? ShapeId { get; set; }

	[XmlElement("PublishedLineName")]
	public string? RouteNumber { get; set; } 

	[XmlElement("OperatorRef")]
	public string? Operator { get; set; }

	[XmlElement("OriginRef")]
	public string? OriginStopId { get; set; }

	[XmlElement("DestinationRef")]
	public string? DestinationStopId { get; set; }

	[XmlElement("DestinationName")]
	public string? DestinationStopName { get; set; }

	[XmlElement("Monitored")]
	public bool? IsMonitored { get; set; }

	[XmlElement("MonitoringError")]
	public string? MonitoringError { get; set; }

	[XmlElement("InCongestion")]
	public bool? IsInCongestion { get; set; }

	[XmlElement("InPanic")]
	public bool? IsInPanic { get; set; }

	[XmlElement("VehicleLocation")]
	public VehicleLocation? Location { get; set; }

	[XmlElement(DataType = "duration")]
	public string? Delay { get; set; }

	[XmlElement("BlockRef")]
	public string? BlockId { get; set; }

	[XmlElement("VehicleRef")]
	public string? VehicleNumber { get; set; }

	[XmlElement("DriverRef")]
	public string? DriverNumber { get; set; }

	[XmlElement("DriverName")]
	public string? DriverName { get; set; }

	[XmlArray("PreviousCalls")]
	[XmlArrayItem("PreviousCall")]
	public PreviousCall[]? PreviousStops { get; set; }

	public MonitoredCall? MonitoredCall { get; set; }

	[XmlArray("OnwardCalls")]
	[XmlArrayItem("OnwardCall")]
	public OnwardCall[]? FutureStops { get; set; }
}
