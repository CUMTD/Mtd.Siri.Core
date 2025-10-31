using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.StopMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	public class VehicleLocation
	{
		public decimal Longitude { get; set; }

		public decimal Latitude { get; set; }
	}
}
