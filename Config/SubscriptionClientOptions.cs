using System.ComponentModel.DataAnnotations;

namespace Mtd.Siri.Core.Config;

public abstract record SubscriptionClientOptions
{
	/// <summary>Where we POST the subscription request.</summary>
	[Required]
	public required Uri SubscribeAddress { get; init; }

	/// <summary>Where the supplier will send data (the delivery endpoint).</summary>
	[Required]
	public required Uri DataSuplyEndpointAddress { get; init; }

	/// <summary>Unique subscriber reference/id.</summary>
	[Required, MinLength(1)]
	public required string SubscriberRef { get; init; }
}
