# Mtd.Siri.Core

[![.NET Build and Test](https://github.com/CUMTD/Mtd.Siri.Core/actions/workflows/built-test.yml/badge.svg)](https://github.com/CUMTD/Mtd.Siri.Core/actions/workflows/built-test.yml)
![GitHub Release](https://img.shields.io/github/v/release/CUMTD/Mtd.Siri.Core?include_prereleases&sort=semver&display_name=release&label=latest%20release)

## GitHub NuGet Feed

See instructions in [Mtd.Core](https://github.com/CUMTD/Mtd.Core) for information about using the GitHub NuGet feed.

## Class Diagrams


### Reqeuests

```mermaid
classDiagram
    %% Root Siri requests
    SiriRequest <|-- RequestResponseRequest_T
    SiriRequest <|-- SubscriptionRequest_Generic_T

    %% ServiceRequest hierarchy
    ServiceRequest <|-- GeneralMessagingServiceRequest
    ServiceRequest <|-- StopMonitoringServiceRequest

    %% SubscriptionRequest (abstract) for payloads
    BaseSubscriptionRequest <|-- VM_SubscriptionRequest

    %% VM subscription envelope
    SubscriptionRequest_Generic_T o-- VM_SubscriptionRequest : Request
    VM_SubscriptionRequest o-- VehicleMonitoringSubscriptionRequest : SubscriptionRequest
    VehicleMonitoringSubscriptionRequest o-- VM_Request : VehicleMonitoringRequest

    %% GM request
    GeneralMessagingServiceRequest o-- GeneralMessageRequest

    %% SM request
    StopMonitoringServiceRequest o-- StopMonitoringRequest : StopPointRequest[0..*]

    %% Data supply (generic)
    SiriRequest <|-- DataSuplySiriRequest
    DataSuplySiriRequest o-- DataSupplyRequest : Request

    %% Context
    ServiceRequest o-- ServiceRequestContext

    class SiriRequest {
        <<abstract>>
        +Version: string
    }

    class RequestResponseRequest_T {
        +Request: T  <<where T: ServiceRequest>>
    }

    class SubscriptionRequest_Generic_T {
        +Request: T  <<where T: BaseSubscriptionRequest>>
    }

    class ServiceRequest {
        <<abstract>>
        +ServiceRequestContext: ServiceRequestContext
        +Timestamp: DateTimeOffset
        +Requestor: string
    }

    class ServiceRequestContext {
        +DataHorizon?: string
        +Timeout?: string
        +ConfirmDelivery: bool
    }

    class GeneralMessagingServiceRequest {
        +GeneralMessageRequest: GeneralMessageRequest
    }

    class GeneralMessageRequest {
        +Timestamp: DateTimeOffset
        +InfoChannel: string
        +Version: string
    }

    class StopMonitoringServiceRequest {
        +MessageIdentifier: string
        +StopPointRequest: StopMonitoringRequest[]
    }

    class StopMonitoringRequest {
        +Timestamp: DateTimeOffset
        +PreviewTime?: string
        +StopPointId: string
        +Version: string
    }

    class BaseSubscriptionRequest {
        <<abstract>>
        +Timestamp: DateTimeOffset
        +Requestor: string
    }

    class VM_SubscriptionRequest {
        +SubscriptionRequest: VehicleMonitoringSubscriptionRequest
        +SetRequestor(requestor): void
    }

    class VehicleMonitoringSubscriptionRequest {
        +SubscriberRef?: string
        +SubscriptionIdentifier: string
        +InitialTerminationTime: DateTimeOffset
        +Request: VM_Request
        +SetRequestor(requestor): void
    }

    class VM_Request {
        +Timestamp: DateTimeOffset
        +Version: string
    }

    class DataSuplySiriRequest {
        +Request: DataSupplyRequest
    }

    class DataSupplyRequest {
        +Timestamp: DateTimeOffset
        +Consumer: string
    }

```


### Responses

### Generic Delivery

```mermaid
```

#### VM

```mermaid
classDiagram
    %% Inheritance
    ServiceDelivery <|-- VehicleMonitoringServiceDelivery

    %% Composition tree
    VehicleMonitoringServiceDelivery o-- VehicleMonitoringDelivery
    VehicleMonitoringDelivery "1" o-- "0..*" VehicleActivity : VehicleActivity
    VehicleActivity o-- VM_MonitoredVehicleJourney : MonitoredVehicleJourney
    VehicleActivity o-- ProgressBetweenStops
    VehicleActivity o-- Extensions
    Extensions o-- OccupancyDataExtension

    VM_MonitoredVehicleJourney o-- FramedVehicleJourneyRef
    VM_MonitoredVehicleJourney o-- VehicleLocation
    VM_MonitoredVehicleJourney o-- VM_MonitoredCall : MonitoredCall
    VM_MonitoredVehicleJourney o-- OnwardCall : FutureStops[0..*]
    VM_MonitoredVehicleJourney o-- PreviousCall : PreviousStops[0..*]

    class ServiceDelivery {
        +Timestamp: DateTime
        +Producer?: string
        +ResponseMessageIdentifier?: string
    }

    class VehicleMonitoringServiceDelivery {
        +Delivery?: VehicleMonitoringDelivery
    }

    class VehicleMonitoringDelivery {
        +Timestamp: DateTime
        +Subscriber?: string
        +Subscription?: string
        +Status?: bool
        +VehicleActivities?: VehicleActivity[]
        +Version: decimal
    }

    class VehicleActivity {
        +RecordedAt: DateTime
        +ItemIdentifier?: string
        +Expiration: DateTimeOffset
        +IsValid: bool
        +VehicleMonitoringRef?: string
        +ProgressBetweenStops?: ProgressBetweenStops
        +VehicleJourney: VM_MonitoredVehicleJourney
        +Extensions?: Extensions
    }

    class VM_MonitoredVehicleJourney {
        +RouteId?: string
        +Direction?: string
        +FramedVehicleJourneyRef?: FramedVehicleJourneyRef
        +ShapeId?: string
        +RouteNumber?: string
        +Operator?: string
        +OriginStopId?: string
        +DestinationStopId?: string
        +DestinationStopName?: string
        +IsMonitored?: bool
        +MonitoringError?: string
        +IsInCongestion?: bool
        +IsInPanic?: bool
        +Location?: VehicleLocation
        +Delay?: string
        +BlockId?: string
        +VehicleNumber?: string
        +DriverNumber?: string
        +DriverName?: string
        +PreviousStops?: PreviousCall[]
        +MonitoredCall?: VM_MonitoredCall
        +FutureStops?: OnwardCall[]
    }

    class FramedVehicleJourneyRef {
        +DataFrameRef: DateTime
        +DatedVehicleJourneyRef: string
    }

    class VehicleLocation {
        +Latitude: decimal
        +Longitude: decimal
    }

    class VM_MonitoredCall {
        +VisitNumber?: int
        +StopName?: string
        +VehicleIsAtStop?: bool
        +Headsign?: string
        +ScheduledArrival?: DateTime
        +ExpectedArrival?: DateTime
        +ScheduledDeparture?: DateTime
        +ExpectedDeparture?: DateTime
    }

    class OnwardCall {
        +VisitNumber: byte
        +StopName: string
        +StopId: string
        +ScheduledArrival: DateTime
        +ExpectedArrival: DateTime
        +ScheduledDeparture: DateTime
        +ExpectedDeparture: DateTime
    }

    class PreviousCall {
        +VisitNumber: byte
        +StopName: string
        +StopId: string
        +ScheduledArrival: DateTime
        +ScheduledDeparture: DateTime
    }

    class ProgressBetweenStops {
        +LinkDistance: ushort
        +Percentage: decimal
    }

    class Extensions {
        +OccupancyData: OccupancyDataExtension
    }

    class OccupancyDataExtension {
        +OccupancyPercentage: float
        +PassengersNumber: int
        +VehicleCapacity: int
        +VehicleSeatsNumber: int
    }
```

#### SM

```mermaid
classDiagram
    %% Inheritance
    ServiceDelivery <|-- RequestResponseServiceDelivery
    RequestResponseServiceDelivery <|-- StopMonitoringServiceDelivery

    %% Composition tree
    StopMonitoringServiceDelivery o-- SM_Result : StopMonitoringDelivery
    SM_Result "1" o-- "0..*" MonitoredStopVisit
    MonitoredStopVisit o-- SM_MonitoredVehicleJourney
    SM_MonitoredVehicleJourney o-- VehicleLocation
    SM_MonitoredVehicleJourney o-- SM_VehicleJourney
    SM_MonitoredVehicleJourney o-- SM_MonitoredCall

    class ServiceDelivery {
        +Timestamp: DateTime
        +Producer?: string
        +ResponseMessageIdentifier?: string
    }

    class RequestResponseServiceDelivery {
        +Status?: bool
        +MoreData?: bool
    }

    class StopMonitoringServiceDelivery {
        +Result?: SM_Result[]
    }

    class SM_Result {
        +Timestamp: DateTime
        +Status: bool
        +Results?: MonitoredStopVisit[]
        +Version: decimal
    }

    class MonitoredStopVisit {
        +RecordedAt: DateTime
        +StopId: string
        +MonitoredVehicleJourney: SM_MonitoredVehicleJourney
    }

    class SM_MonitoredVehicleJourney {
        +RouteId?: string
        +Direction?: string
        +VehicleJourney?: SM_VehicleJourney
        +ShapeId?: string
        +Number?: string
        +Operator?: string
        +OriginStopId?: string
        +DestinationStopId?: string
        +DestinationStopName?: string
        +IsRealtime?: bool
        +IsInCongestion?: bool
        +Location?: VehicleLocation
        +Delay?: string
        +BlockId?: string
        +VehicleNumber?: string
        +Realtime?: SM_MonitoredCall
    }

    class SM_VehicleJourney {
        +DataFrame: DateTime
        +TripPrefix: string
    }

    class VehicleLocation {
        +Longitude: decimal
        +Latitude: decimal
    }

    class SM_MonitoredCall {
        +VisitNumber?: int
        +IsVehicleAtStop?: bool
        +Headsign?: string
        +ScheduledArrival?: DateTime
        +EstimatedArrival?: DateTime
        +ScheduledDeparture?: DateTime
        +EstimatedDeparture?: DateTime
        +DeparturePlatform?: string
        +ScheduleAdherence?: TimeSpan
    }

```


#### GM

```mermaid
classDiagram
    %% Inheritance
    ServiceDelivery <|-- RequestResponseServiceDelivery
    RequestResponseServiceDelivery <|-- GeneralMessageServiceDelivery

    %% Composition tree
    GeneralMessageServiceDelivery o-- GM_Result : GeneralMessageDelivery
    GM_Result "1" o-- "0..*" GeneralMessage
    GeneralMessage o-- Content
    Content o-- Message
    Message o-- MessageText

    class ServiceDelivery {
        +Timestamp: DateTime
        +Producer?: string
        +ResponseMessageIdentifier?: string
    }

    class RequestResponseServiceDelivery {
        +Status?: bool
        +MoreData?: bool
    }

    class GeneralMessageServiceDelivery {
        +Result?: GM_Result
    }

    class GM_Result {
        +Version: decimal
        +Timestamp: DateTime
        +Status: bool
        +Messages?: GeneralMessage[]
    }

    class GeneralMessage {
        +Id: string
        +Timestamp: DateTime
        +InfoChannel?: string
        +Content: Content
    }

    class Content {
        +Message?: Message  <<INIT extension>>
    }

    class Message {
        +Id: string
        +Created: DateTime
        +DisplayId?: string
        +Text?: MessageText
        +IsPeriodical?: bool
        +StartDay?: DateTime
        +EndDay?: DateTime
        +StartTime?: DateTime
        +EndTime?: DateTime
        +HorizontalAlingment?: string
        +Priority?: string
        +StopIds?: string[]
        +BlockRealtime: bool  <<computed>>
    }

    class MessageText {
        +Lines?: string[]
    }

```


```mermaid

```


```mermaid

```


```mermaid

```

```mermaid

```