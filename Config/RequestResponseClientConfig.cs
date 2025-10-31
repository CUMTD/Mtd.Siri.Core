using System.ComponentModel.DataAnnotations;

namespace Mtd.Siri.Core.Config;

public abstract record RequestResponseClientOptions
{
	[Required]
	public required Uri Endpoint { get; init; }

	public bool LogResponse { get; init; } = false;
}
