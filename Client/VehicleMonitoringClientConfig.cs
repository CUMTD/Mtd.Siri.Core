using Mtd.Siri.Core.Client.Generic;

namespace Mtd.Siri.Core.Client
{
	public sealed record VehicleMonitoringClientConfig(string SubscribeAddress, string DataSuplyEndpointAddress, string SubscriberRef, int SubscriptionIntervalMinutes) :
		SubscriptionClientConfig(SubscribeAddress, DataSuplyEndpointAddress, SubscriberRef);
}
