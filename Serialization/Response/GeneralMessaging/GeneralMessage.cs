using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.GeneralMessaging
{
	[Serializable]
	[XmlType("GeneralMessage", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class GeneralMessage
	{
		[XmlElement("InfoMessageIdentifier")]
		public string Id { get; set; }

		[XmlElement("RecordedAtTime")]
		public DateTime Timestamp { get; set; }

		[XmlElement("InfoChannelRef")]
		public string InfoChannel { get; set; }

		[XmlElement("Content")]
		public Content Content { get; set; }
	}
}