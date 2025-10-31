using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response
{
	[Serializable]
	[XmlType("ServiceDelivery", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public abstract class RequestResponseServiceDelivery : ServiceDelivery
	{
		public bool Status { get; set; }

		public bool MoreData { get; set; }
	}
}