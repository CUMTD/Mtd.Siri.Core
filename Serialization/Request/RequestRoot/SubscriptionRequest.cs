using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.RequestRoot
{
	[Serializable]
	[XmlType("Siri", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	[XmlRoot("Siri", Namespace = "http://www.siri.org.uk/", IsNullable = false)]
	public class SubscriptionRequest<T> : SiriRequest
		 where T : SubscriptionRequest, new()
	{
		[XmlElement("SubscriptionRequest")]
		public T Request { get; set; }

		public SubscriptionRequest()
		{
			Request = new T();
		}
	}
}
