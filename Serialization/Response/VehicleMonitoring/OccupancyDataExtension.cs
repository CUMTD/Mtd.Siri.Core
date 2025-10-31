using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.VehicleMonitoring
{
	[Serializable]
	[XmlType(AnonymousType = true, Namespace = "http://www.init-ka.de/occupancy")]
	[XmlRoot(Namespace = "http://www.init-ka.de/occupancy", IsNullable = false)]
	public class OccupancyDataExtension
	{
		public float OccupancyPercentage { get; set; }
		public int PassengersNumber { get; set; }
		public int VehicleCapacity { get; set; }
		public int VehicleSeatsNumber { get; set; }
	}
}
