using Mtd.Siri.Core.Client.Generic;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Request.StopMonitoring;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;
using Mtd.Siri.Core.Serialization.Response.StopMonitoring;
using Mtd.Stopwatch.Core.Entities.Realtime;
using Microsoft.Extensions.Logging;

namespace Mtd.Siri.Core.Client
{
	public interface IStopMonitoringClient
	{
		Task<IEnumerable<Departure>> GetDeparturesForStopPoints(CancellationToken cancellationToken, params string[] stopPointIds);
		Task<IEnumerable<Departure>> GetDeparturesForStopPoints(int previewMinutes, CancellationToken cancellationToken, params string[] stopPointIds);
	}

	public sealed class StopMonitoringClient(StopMonitoringClientConfig config, HttpClient httpClient, ILogger<StopMonitoringClient> logger)
		: RequestResponseClient<StopMonitoringServiceRequest, StopMonitoringServiceDelivery, Departure>(config, httpClient, logger), IStopMonitoringClient
	{
		public Task<IEnumerable<Departure>> GetDeparturesForStopPoints(CancellationToken cancellationToken, params string[] stopPointIds) => GetDeparturesForStopPoints(30, cancellationToken, stopPointIds);

		public Task<IEnumerable<Departure>> GetDeparturesForStopPoints(int previewMinutes, CancellationToken cancellationToken, params string[] stopPointIds)
		{
			var stopPoints = stopPointIds
				.Select(spId => new StopMonitoringRequest(spId, previewMinutes))
				.ToArray();

			_logger.LogDebug("{method} requesting SM for: {stopIds}", nameof(GetDeparturesForStopPoints), string.Join(", ", stopPointIds));
			var request = new RequestResponseRequest<StopMonitoringServiceRequest>
			{
				Request =
				{
					StopPointRequest = stopPoints
				}
			};

			return RequestData(request, cancellationToken);
		}

		protected override IEnumerable<Departure> ConvertResponse(SiriResponse<StopMonitoringServiceDelivery> siriResponse) => siriResponse
				.ServiceDelivery
				.Result
				.Where(r => r.Results != default) // because siri returns an empty StopMonitoringDelivery if there are no departures for a stop point
				.SelectMany(r => r.Results)
				.Select(ToDeparture)
				.Where(d => d != default)
				.Select(d => d!)
				.OrderBy(d => d.EstimatedDeparture);

		private static Departure? ToDeparture(MonitoredStopVisit stopVisit) => stopVisit?.MonitoredVehicleJourney?.Realtime == default
				? default
				: new Departure
				{
					StopId = stopVisit.StopId.Trim(),
					Headsign = stopVisit.MonitoredVehicleJourney.Realtime?.Headsign?.Trim(),
					RouteId = stopVisit.MonitoredVehicleJourney?.RouteId?.Trim(),
					RouteNumber = stopVisit.MonitoredVehicleJourney?.Number.Trim(),
					Direction = stopVisit.MonitoredVehicleJourney?.Direction.Trim(),
					OriginStopId = stopVisit.MonitoredVehicleJourney?.OriginStopId?.Trim(),
					DestinationStopId = stopVisit.MonitoredVehicleJourney?.DestinationStopId.Trim(),
					Destination = stopVisit.MonitoredVehicleJourney?.DestinationStopName.Trim(),
					VehicleId = stopVisit.MonitoredVehicleJourney?.VehicleNumber.Trim() ?? "UNKNOWN",
					Latitude = stopVisit.MonitoredVehicleJourney?.Location?.Latitude ?? 0,
					Longitude = stopVisit.MonitoredVehicleJourney?.Location?.Longitude ?? 0,
					ShapeId = stopVisit.MonitoredVehicleJourney?.ShapeId?.Trim(),
					BlockId = stopVisit.MonitoredVehicleJourney?.BlockId?.Trim(),
					TripPrefix = stopVisit.MonitoredVehicleJourney?.VehicleJourney?.TripPrefix?.Trim(),
					RecordedTime = stopVisit.RecordedAt,
					ScheduledDeparture = stopVisit.MonitoredVehicleJourney.Realtime.ScheduledDeparture,
					EstimatedDeparture = stopVisit.MonitoredVehicleJourney.Realtime.EstimatedDeparture,
					IsRealTime = stopVisit.MonitoredVehicleJourney.IsRealtime
				};
	}
}
