using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.RequestRoot
{
	[Serializable]
	[XmlType("Siri", AnonymousType = true, Namespace = "http://www.siri.org.uk/")]
	[XmlRoot("Siri", Namespace = "http://www.siri.org.uk/", IsNullable = false)]
	public class DataSuplySiriRequest : SiriRequest
	{
		[XmlElement("DataSupplyRequest")]
		public DataSupplyRequest Request { get; set; }

		public DataSuplySiriRequest()
		{
			Request = new DataSupplyRequest();
		}
	}
}
