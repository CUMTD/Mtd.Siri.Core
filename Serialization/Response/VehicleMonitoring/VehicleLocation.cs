using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;

[Serializable]
[XmlType("VehicleLocation", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
public class VehicleLocation
{
	public decimal Latitude { get; set; }

	public decimal Longitude { get; set; }
}
