using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.RequestRoot
{
	[Serializable]
	[XmlType("Siri", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	[XmlRoot("Siri", Namespace = "http://www.siri.org.uk/", IsNullable = false)]
	public class RequestResponseRequest<T> : SiriRequest
		where T : ServiceRequest, new()
	{
		[XmlElement("ServiceRequest")]
		public T Request { get; set; }

		public RequestResponseRequest()
		{
			Request = new T();
		}
	}
}