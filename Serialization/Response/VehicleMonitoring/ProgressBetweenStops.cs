using System;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType("ProgressBetweenStops", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class ProgressBetweenStops
	{
		public ushort LinkDistance { get; set; }

		public decimal Percentage { get; set; }
	}
}