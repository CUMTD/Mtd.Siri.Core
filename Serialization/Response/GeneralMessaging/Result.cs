using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.GeneralMessaging
{
	public class Result
	{
		[XmlAttribute("version")]
		public decimal Version { get; set; }

		[XmlElement("ResponseTimestamp")]
		public DateTime Timestamp { get; set; }

		[XmlElement("Status")]
		public bool Status { get; set; }

		[XmlElement("GeneralMessage")]
		public GeneralMessage[] Messages { get; set; }
	}
}