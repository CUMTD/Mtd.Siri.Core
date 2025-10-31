using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class MonitoredVehicleJourney
	{
		[XmlElement("LineRef")]
		public string RouteId { get; set; }

		[XmlElement("DirectionRef")]
		public string Direction { get; set; }

		[XmlElement("FramedVehicleJourneyRef")]
		public VehicleJourney VehicleJourney { get; set; }

		[XmlElement("JourneyPatternRef")]
		public string ShapeId { get; set; }

		[XmlElement("PublishedLineName")]
		public string Number { get; set; }

		[XmlElement("OperatorRef")]
		public string Operator { get; set; }

		[XmlElement("OriginRef")]
		public string OriginStopId { get; set; }

		[XmlElement("DestinationRef")]
		public string DestinationStopId { get; set; }

		[XmlElement("DestinationName")]
		public string DestinationStopName { get; set; }

		[XmlElement("Monitored")]
		public bool IsRealtime { get; set; }

		[XmlElement("InCongestion")]
		public bool IsInCongestion { get; set; }

		[XmlElement("VehicleLocation")]
		public VehicleLocation Location { get; set; }

		[XmlElement(DataType = "duration")]
		public string Delay { get; set; }

		[XmlElement("BlockRef")]
		public string BlockId { get; set; }

		[XmlElement("VehicleRef")]
		public string VehicleNumber { get; set; }

		[XmlElement("MonitoredCall")]
		public MonitoredCall Realtime { get; set; }
	}
}