using System.Xml.Serialization;

namespace Mtd.Siri.Core.Serialization.Request.VehicleMonitoring
{
	public class VMSubscriptionRequest : SubscriptionRequest
	{
		[XmlElement("VehicleMonitoringSubscriptionRequest")]
		public VehicleMonitoringSubscriptionRequest SubscriptionRequest { get; set; }

		public VMSubscriptionRequest()
		{
			SubscriptionRequest = new VehicleMonitoringSubscriptionRequest();
		}

		public override void SetRequestor(string requestor)
		{
			base.SetRequestor(requestor);
			SubscriptionRequest?.SetRequestor(requestor);
		}
	}
}
