using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType("FramedVehicleJourneyRef", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class FramedVehicleJourneyRef
	{
		[XmlElement(DataType = "date")]
		public DateTime DataFrameRef { get; set; }

		public string DatedVehicleJourneyRef { get; set; }
	}
}