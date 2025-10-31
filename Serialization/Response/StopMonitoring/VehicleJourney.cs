using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class VehicleJourney
	{
		[XmlElement("DataFrameRef", DataType = "date")]
		public DateTime DataFrame { get; set; }

		[XmlElement("DatedVehicleJourneyRef")]
		public string TripPrefix { get; set; } = default!;
	}
}
