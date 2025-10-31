using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.StopMonitoring;

public class StopMonitoringServiceRequest : ServiceRequest
{
	public string MessageIdentifier { get; set; }

	[XmlElement("StopMonitoringRequest")]
	public StopMonitoringRequest[] StopPointRequest { get; set; }

	public StopMonitoringServiceRequest() : base("cumtdrr")
	{
		MessageIdentifier = "0caaa79f";
		StopPointRequest = [];
	}
}
