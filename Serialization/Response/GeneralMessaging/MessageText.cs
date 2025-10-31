using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.GeneralMessaging
{
	[Serializable]
	[XmlType("text", AnonymousType = true, Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
	public class MessageText
	{
		[XmlElement("textLine", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string[]? Lines { get; set; }
	}
}
