using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType("Extensions", AnonymousType = true)]

	public class Extensions
	{
		[XmlElement(Namespace = "http://www.init-ka.de/occupancy")]
		public OccupancyDataExtension? OccupancyData { get; set; }

		[XmlAnyElement]
		public System.Xml.XmlElement[]? UnknownElements { get; set; }
	}
}

