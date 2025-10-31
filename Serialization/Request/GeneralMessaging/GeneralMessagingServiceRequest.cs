namespace Mtd.Siri.Core.Serialization.Request.GeneralMessaging;

public class GeneralMessagingServiceRequest : ServiceRequest
{
	public GeneralMessageRequest GeneralMessageRequest { get; set; }

	public GeneralMessagingServiceRequest() : base("cumtdgm")
	{
		GeneralMessageRequest = new GeneralMessageRequest();
	}
}