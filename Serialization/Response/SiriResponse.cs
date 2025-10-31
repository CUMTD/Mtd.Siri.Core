using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response
{
	[Serializable]
	[XmlType("Siri", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	[XmlRoot("Siri", Namespace = "http://www.siri.org.uk/", IsNullable = false)]
	public class SiriResponse<T>
		where T : ServiceDelivery
	{
		public T ServiceDelivery { get; set; }

		[XmlAttribute("version")]
		public decimal Version { get; set; }
	}
}