using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request;

[Serializable]
[XmlType("ServiceRequest", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public abstract class ServiceRequest
{
	[XmlElement("ServiceRequestContext")]
	public ServiceRequestContext ServiceRequestContext { get; set; }

	[XmlElement("RequestTimestamp")]
	public DateTimeOffset Timestamp { get; set; }

	[XmlElement("RequestorRef")]
	public string Requestor { get; set; } = string.Empty!;

	protected ServiceRequest()
	{
		Timestamp = TimeProvider.System.GetLocalNow();
		ServiceRequestContext = new ServiceRequestContext();
	}

	protected ServiceRequest(string requestor) : this()
	{
		Requestor = requestor;
	}
}
