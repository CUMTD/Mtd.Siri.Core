using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType("VehicleMonitoringDelivery", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class VehicleMonitoringDelivery
	{
		[XmlElement("ResponseTimestamp")]
		public DateTime Timestamp { get; set; }

		[XmlElement("SubscriberRef")]
		public string? Subscriber { get; set; }

		[XmlElement("SubscriptionRef")]
		public string? Subscription { get; set; }

		public bool? Status { get; set; }

		[XmlElement("VehicleActivity")]
		public VehicleActivity[]? VehicleActivities { get; set; }

		[XmlAttribute("version")]
		public decimal Version { get; set; }
	}
}
