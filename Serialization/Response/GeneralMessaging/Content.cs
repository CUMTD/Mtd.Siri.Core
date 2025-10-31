using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.GeneralMessaging
{
	[Serializable]
	[XmlType("Content", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class Content
	{
		[XmlElement("generalSpecialTextDisplayMessage", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public Message? Message { get; set; }
	}
}
