using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.StopMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class StopMonitoringRequest
	{
		[XmlElement("RequestTimestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[XmlElement("PreviewInterval", DataType = "duration")]
		public string PreviewTime { get; set; }

		[XmlElement("MonitoringRef")]
		public string StopPointId { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		public StopMonitoringRequest()
		{
			Timestamp = TimeProvider.System.GetLocalNow();
			Version = "1.0";
		}

		public StopMonitoringRequest(string stopPointId, int previewMinutes = 30) : this()
		{
			var pt = Math.Min(Math.Max(30, previewMinutes), 60);
			PreviewTime = $"PT{pt}M";
			StopPointId = stopPointId;
		}
	}
}
