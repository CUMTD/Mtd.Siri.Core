using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.RequestRoot
{
	[Serializable]
	[XmlType("Siri", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	[XmlRoot("Siri", Namespace = "http://www.siri.org.uk/", IsNullable = false)]
	public abstract class SiriRequest(string version)
	{
		[XmlAttribute("version")]
		public string Version { get; set; } = version;

		protected SiriRequest() : this("1.0")
		{
		}
	}
}
