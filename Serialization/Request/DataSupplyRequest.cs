using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request;

[Serializable]
[XmlType("DataSupplyRequest", AnonymousType = true)]
public class DataSupplyRequest
{
	[XmlElement("RequestTimestamp")]
	public DateTimeOffset Timestamp { get; set; }

	[XmlElement("ConsumerRef")]
	public string Consumer { get; set; }

	public DataSupplyRequest() : this("cumtd")
	{
	}

	public DataSupplyRequest(string consumer)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(consumer);

		Timestamp = TimeProvider.System.GetLocalNow();
		Consumer = consumer;
	}
}
