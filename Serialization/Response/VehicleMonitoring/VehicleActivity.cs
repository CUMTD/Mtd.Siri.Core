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
		public string ItemIdentifier { get; set; }

		[XmlElement("ValidUntilTime")]
		public DateTimeOffset Experation { get; set; }

		public bool IsValid => Experation > TimeProvider.System.GetLocalNow();

		[XmlElement("VehicleMonitoringRef")]
		public string VehicleMonitoringRef { get; set; }

		public ProgressBetweenStops ProgressBetweenStops { get; set; }

		[XmlElement("MonitoredVehicleJourney")]
		public MonitoredVehicleJourney VehicleJourney { get; set; }

		[XmlElement("Extensions")]
		public Extensions Extensions { get; set; }
	}
}
