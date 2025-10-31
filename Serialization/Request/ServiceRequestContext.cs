using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request
{
	[Serializable]
	[XmlType("ServiceRequestContext", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class ServiceRequestContext
	{
		[XmlElement(DataType = "duration")]
		public string DataHorizon { get; set; }

		[XmlElement("RequestTimeout", DataType = "duration")]
		public string Timeout { get; set; }

		public bool ConfirmDelivery { get; set; }

		public ServiceRequestContext()
		{
			DataHorizon = "P1Y2M3DT10H30M";
			Timeout = "P1Y2M3DT10H30M";
			ConfirmDelivery = true;
		}
	}
}