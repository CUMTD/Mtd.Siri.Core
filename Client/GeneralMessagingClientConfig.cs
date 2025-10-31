using Mtd.Siri.Core.Client.Generic;

namespace Mtd.Siri.Core.Client;

public sealed record GeneralMessagingClientConfig(string Endpoint, bool LogResponse) : RequestResponseClientConfig(Endpoint, LogResponse);
