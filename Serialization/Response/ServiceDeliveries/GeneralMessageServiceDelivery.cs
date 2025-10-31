using Mtd.Siri.Core.Serialization.Response.GeneralMessaging;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;

public class GeneralMessageServiceDelivery : RequestResponseServiceDelivery
{
	[XmlElement("GeneralMessageDelivery")]
	public Result? Result { get; set; }
}
