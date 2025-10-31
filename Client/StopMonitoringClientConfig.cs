using Mtd.Siri.Core.Client.Generic;

namespace Mtd.Siri.Core.Client;

public sealed record StopMonitoringClientConfig(string Endpoint, bool LogResponse) : RequestResponseClientConfig(Endpoint, LogResponse);
