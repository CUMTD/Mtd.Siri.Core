using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client;
public interface IGeneralMessagingClient
{
	Task<IEnumerable<Message>> GetAllMessagesAsync(CancellationToken cancellationToken);
}
