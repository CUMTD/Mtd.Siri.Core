using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client;
public interface IStopMonitoringClient
{
	Task<IEnumerable<Departure>> GetDeparturesForStopPointsAsync(CancellationToken cancellationToken, params string[] stopPointIds);
	Task<IEnumerable<Departure>> GetDeparturesForStopPointsAsync(int previewMinutes, CancellationToken cancellationToken, params string[] stopPointIds);
}
