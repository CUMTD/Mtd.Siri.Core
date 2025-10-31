using Mtd.Siri.Core.Serialization.Request;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client.Generic;
public interface ISubscriptionClient<TSubscriptionRequest, TResult>
	where TSubscriptionRequest : SubscriptionRequest, new()
	where TResult : IRealtimeData
{
	Task<IEnumerable<TResult>> GetDataReadyResultsAsync(CancellationToken cancellationToken);
	Task<SubscriptionRequest<TSubscriptionRequest>> SubscribeAsync(CancellationToken cancellationToken);
}
