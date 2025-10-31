using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response
{
	[Serializable]
	[XmlType(Namespace = "http://www.siri.org.uk/")]
	public partial class SubscriptionResponseStatus
	{
		[XmlElement]
		public DateTime ResponseTimestamp { get; set; }
		[XmlElement]
		public string? SubscriberRef { get; set; }

		[XmlElement]
		public string? SubscriptionRef { get; set; }
		[XmlElement]
		public bool Status { get; set; }
	}

	/// <remarks/>
	[Serializable()]
	[XmlType(Namespace = "http://www.siri.org.uk/")]
	public class SubscriptionResponse
	{
		[XmlElement]
		public DateTime ResponseTimestamp { get; set; }

		[XmlElement]
		public string? ResponderRef { get; set; }

		public SubscriptionResponseStatus ResponseStatus { get; set; } = default!;

		[XmlElement]
		public DateTime ServiceStartedTime { get; set; }
	}

	[Serializable()]
	[XmlType(Namespace = "http://www.siri.org.uk/")]
	[XmlRoot(Namespace = "http://www.siri.org.uk/", IsNullable = false, ElementName = "Siri")]
	public partial class SiriSubscriptionResponseContainer
	{
		[XmlElement]
		public SubscriptionResponse? SubscriptionResponse { get; set; }

		[XmlAttribute("version")]
		public string? Version { get; set; } = string.Empty!;
	}
}
