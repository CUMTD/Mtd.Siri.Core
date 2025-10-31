using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.GeneralMessaging;

[Serializable]
[XmlType("GeneralMessageRequest", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class GeneralMessageRequest
{
	[XmlElement("RequestTimestamp")]
	public DateTimeOffset Timestamp { get; set; }

	[XmlElement("InfoChannelRef")]
	public string InfoChannel { get; set; }

	[XmlAttribute("version")]
	public string Version { get; set; }

	public GeneralMessageRequest()
	{
		Timestamp = TimeProvider.System.GetLocalNow();
		InfoChannel = "TEXT";
		Version = "1.0";
	}
}
