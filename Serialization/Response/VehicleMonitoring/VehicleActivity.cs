using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	public class VehicleActivity
	{
		[XmlElement("RecordedAtTime")]
		public DateTime RecordedAt { get; set; }

		[XmlElement("ItemIdentifier")]
		public string? ItemIdentifier { get; set; }

		[XmlElement("ValidUntilTime")]
		public DateTimeOffset Expiration { get; set; }

		[XmlIgnore]
		public bool IsValid => Expiration > TimeProvider.System.GetLocalNow();

		[XmlElement("VehicleMonitoringRef")]
		public string? VehicleMonitoringRef { get; set; }

		public ProgressBetweenStops? ProgressBetweenStops { get; set; }

		[XmlElement("MonitoredVehicleJourney")]
		public MonitoredVehicleJourney VehicleJourney { get; set; } = default!; // required in practice

		[XmlElement("Extensions")]
		public Extensions? Extensions { get; set; }
	}
}
