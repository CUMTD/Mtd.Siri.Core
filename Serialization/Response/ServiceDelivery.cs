using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response
{
	[Serializable]
	[XmlRoot("ServiceDelivery", Namespace = "http://www.siri.org.uk/")]
	public abstract class ServiceDelivery
	{
		[XmlElement("ResponseTimestamp")]
		public DateTime Timestamp { get; set; }

		[XmlElement("ProducerRef")]
		public string Producer { get; set; }

		public string ResponseMessageIdentifier { get; set; }
	}
}
