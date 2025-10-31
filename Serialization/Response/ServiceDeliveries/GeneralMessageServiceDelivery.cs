using System.Xml.Serialization;
using Mtd.Siri.Core.Serialization.Response.GeneralMessaging;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries
{
	public class GeneralMessageServiceDelivery : RequestResponseServiceDelivery
	{
		[XmlElement("GeneralMessageDelivery")]
		public Result Result { get; set; }
	}
}