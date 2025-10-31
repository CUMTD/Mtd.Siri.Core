using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.VehicleMonitoring;

[Serializable]
[XmlType("VehicleMonitoringSubscriptionRequest", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class VehicleMonitoringSubscriptionRequest(DateTimeOffset initialTerminationTime)
{
	[XmlElement("SubscriberRef")]
	public string SubscriberRef { get; set; } = "cumtd";

	[XmlElement("SubscriptionIdentifier")]
	public string SubscriptionIdentifier { get; set; } = "cumtd";

	[XmlElement("InitialTerminationTime")]
	public DateTimeOffset InitialTerminationTime { get; set; } = initialTerminationTime;

	[XmlElement("VehicleMonitoringRequest")]
	public Request Request { get; set; } = new Request();

	public VehicleMonitoringSubscriptionRequest() : this(TimeProvider.System.GetLocalNow().AddDays(1))
	{
	}

	public void SetRequestor(string requestor)
	{
		SubscriberRef = requestor;
		SubscriptionIdentifier = requestor;
	}
}
