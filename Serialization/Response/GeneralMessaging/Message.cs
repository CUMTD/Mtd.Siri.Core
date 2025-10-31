using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.GeneralMessaging
{
	[Serializable]
	[XmlType("generalSpecialTextDisplayMessage", AnonymousType = true, Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
	[XmlRoot("generalSpecialTextDisplayMessage", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND", IsNullable = false)]
	public class Message
	{
		[XmlElement("id", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string Id { get; set; } = default!;

		[XmlElement("creationTime", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public DateTime Created { get; set; }

		[XmlElement("displayId", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string? DisplayId { get; set; }

		[XmlElement("text", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public MessageText? Text { get; set; }

		[XmlElement("periodical", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public bool? IsPeriodical { get; set; }

		[XmlElement("firstDay", DataType = "date", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public DateTime? StartDay { get; set; }

		[XmlElement("lastDay", DataType = "date", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public DateTime? EndDay { get; set; }

		[XmlElement("startTime", DataType = "time", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public DateTime? StartTime { get; set; }

		[XmlElement("endTime", DataType = "time", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public DateTime? EndTime { get; set; }

		[XmlElement("horizontalAlingment", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string? HorizontalAlingment { get; set; }

		[XmlElement("priority", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string? Priority { get; set; }

		[XmlIgnore]
		public bool BlockRealtime => string.Equals(Priority, "EXCLUSIVE", StringComparison.OrdinalIgnoreCase);

		[XmlElement("stopId", Namespace = "http://www.init-ka.de/vdv/vdv453InitAND")]
		public string[]? StopIds { get; set; }
	}
}
