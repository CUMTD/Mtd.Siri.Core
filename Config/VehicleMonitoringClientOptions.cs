using System.ComponentModel.DataAnnotations;

namespace Mtd.Siri.Core.Config;

public sealed record VehicleMonitoringClientOptions : SubscriptionClientOptions
{
	/// <summary>How long (in minutes) the subscription should last before renewal.</summary>
	[Range(1, 10080)] // up to 7 days;
	public int SubscriptionIntervalMinutes { get; init; }
}
