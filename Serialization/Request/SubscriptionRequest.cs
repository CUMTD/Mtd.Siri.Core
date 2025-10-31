using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request;

[Serializable]
[XmlType("SubscriptionRequest", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public abstract class SubscriptionRequest
{
	[XmlElement("RequestTimestamp")]
	public DateTimeOffset Timestamp { get; set; }

	[XmlElement("RequestorRef")]
	public string Requestor { get; set; } = string.Empty!;

	protected SubscriptionRequest()
	{
		Timestamp = TimeProvider.System.GetLocalNow();
	}

	public virtual void SetRequestor(string requestor) => Requestor = requestor;
}
