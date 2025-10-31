using Microsoft.Extensions.Logging;
using Mtd.Siri.Core.Client.Generic;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Request.VehicleMonitoring;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;
using Mtd.Siri.Core.Serialization.Response.VehicleMonitoring;
using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client;

public sealed class VehicleMonitoringClient : SubscriptionClient<VMSubscriptionRequest, VehicleMonitoringServiceDelivery, MonitoredVehicle>
{
	private readonly int _subscriptionIntervalMinutes;

	public VehicleMonitoringClient(VehicleMonitoringClientConfig config, HttpClient httpClient, ILogger<VehicleMonitoringClient> logger)
		: base(config, httpClient, logger)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));
		ArgumentOutOfRangeException.ThrowIfLessThan(config.SubscriptionIntervalMinutes, 1, nameof(config.SubscriptionIntervalMinutes));

		_subscriptionIntervalMinutes = config.SubscriptionIntervalMinutes;
	}

	protected override IEnumerable<MonitoredVehicle> ConvertResponse(SiriResponse<VehicleMonitoringServiceDelivery> siriResponse)
	{
		var vehicleActivities = siriResponse
			?.ServiceDelivery
			?.Delivery
			?.VehicleActivities;

		if (vehicleActivities == null)
		{
			_logger.LogWarning("VehicleActivities was null in VehicleMonitoringClient.ConvertResponse.");
			return [];
		}

		return vehicleActivities
			.Select(ConvertVehicleActivity)
			.Where(mv => mv != null)
			.Select(mv => mv!);
	}

	protected override SubscriptionRequest<VMSubscriptionRequest> CreateRequest()
	{
		var request = base.CreateRequest();
		request.Request.SubscriptionRequest.InitialTerminationTime = TimeProvider.System.GetLocalNow().AddMinutes(_subscriptionIntervalMinutes);
		return request;
	}

	private MonitoredVehicle? ConvertVehicleActivity(VehicleActivity vehicleActivity)
	{
		var nullableVehicleJourney = vehicleActivity?.VehicleJourney;

		if (nullableVehicleJourney == default)
		{
			_logger.LogWarning("{item} was null in {param}.", nameof(MonitoredVehicleJourney), nameof(vehicleActivity));
			return null;
		}
		else if (string.IsNullOrEmpty(nullableVehicleJourney.VehicleNumber))
		{
			// we don't want to do anything if the vehicle number isn't set.
			// this should never happen, but just in case.
			return null;
		}

		var vehicleJourney = nullableVehicleJourney!;
		var previousStop = vehicleJourney
			.PreviousStops
			?.OrderByDescending(ps => ps.VisitNumber)
			.FirstOrDefault();

		var nextStop = vehicleJourney
			.FutureStops
			?.OrderBy(ps => ps.VisitNumber)
			.FirstOrDefault();

		// Set trip id
		// account for trip ids that already have the block part appened (__<BLOCK_ID>) and trip ids that do not.
		string? tripId = null;
		if (!string.IsNullOrEmpty(vehicleJourney.FramedVehicleJourneyRef?.DatedVehicleJourneyRef))
		{
			var tripPrefix = vehicleJourney.FramedVehicleJourneyRef.DatedVehicleJourneyRef;
			var blockPart = vehicleJourney.BlockId?.Replace(" ", "_");
			tripId = tripPrefix.Contains("__") || string.IsNullOrEmpty(blockPart) ? tripPrefix : $"{tripPrefix}__{blockPart}";
		}

		// vehicleJourney is not null here.
		// it is checked above.
		return new MonitoredVehicle
		{
			RealTime = vehicleJourney.MonitoredCall == default || vehicleJourney.MonitoredCall?.ScheduledDeparture == null ? null : new MonitoredVehicleRealTime
			{
				ExpectedArrival = vehicleJourney!.MonitoredCall?.ExpectedArrival,
				ExpectedDeparture = vehicleJourney!.MonitoredCall?.ExpectedDeparture,
				ScheduledArrival = vehicleJourney!.MonitoredCall?.ScheduledArrival,
				ScheduledDeparture = vehicleJourney!.MonitoredCall!.ScheduledDeparture.Value
			},
			BlockId = vehicleJourney!.BlockId,
			Direction = vehicleJourney!.Direction,
			Headsign = vehicleJourney?.MonitoredCall?.Headsign ?? default,
			IsCanceled = false,
			IsInCongestion = vehicleJourney!.IsInCongestion ?? false,
			IsInPanic = vehicleJourney.IsInPanic ?? false,
			IsRealtime = vehicleJourney.IsMonitored ?? false,
			Latitude = vehicleJourney?.Location?.Latitude ?? -1,
			Longitude = vehicleJourney?.Location?.Longitude ?? -1,
			MonitoringError = vehicleJourney!.MonitoringError,
			NextStop = nextStop == null ? null : new MonitoredVehicleItinerary(nextStop!.StopId, nextStop!.StopName),
			OriginStop = string.IsNullOrEmpty(vehicleJourney.OriginStopId) ? null : new MonitoredVehicleItinerary(vehicleJourney!.OriginStopId, null),
			DestinationStop = string.IsNullOrEmpty(vehicleJourney.DestinationStopId) ? null : new MonitoredVehicleItinerary(vehicleJourney!.DestinationStopId, vehicleJourney!.DestinationStopName),
			PreviousStop = string.IsNullOrEmpty(previousStop?.StopId) ? null : new MonitoredVehicleItinerary(previousStop!.StopId, previousStop!.StopName),
			RouteId = vehicleJourney!.RouteId,
			RouteNumber = vehicleJourney!.RouteNumber,
			TripId = tripId,
			Updated = TimeProvider.System.GetLocalNow(),
			VehicleNumber = vehicleJourney.VehicleNumber,
			DriverNumber = vehicleJourney.DriverNumber?.TrimStart('0'),
			DriverName = vehicleJourney.DriverName,
			Occupancy = new MonitoredVehicleOccupancy
			{
				Capacity = vehicleActivity?.Extensions?.OccupancyData?.VehicleCapacity ?? 0,
				OccupancyPercentage = vehicleActivity?.Extensions?.OccupancyData?.OccupancyPercentage ?? 0,
				PassengerCount = vehicleActivity?.Extensions?.OccupancyData?.PassengersNumber ?? 0,
				SeatCount = vehicleActivity?.Extensions?.OccupancyData?.VehicleSeatsNumber ?? 0,
			}
		};
	}
}
