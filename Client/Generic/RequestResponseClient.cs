using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mtd.Siri.Core.Config;
using Mtd.Siri.Core.Serialization.Request;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client.Generic;

public abstract class RequestResponseClient<T_ServiceRequest, T_ServiceDelivery, T_Result> : Client<T_ServiceDelivery, T_Result>
	where T_ServiceRequest : ServiceRequest, new()
	where T_ServiceDelivery : RequestResponseServiceDelivery
	where T_Result : IRealtimeData
{
	private readonly Uri _endpointAddress;
	private readonly bool _logResponse;

	protected RequestResponseClient(IOptions<RequestResponseClientOptions> config, HttpClient httpClient, ILogger<RequestResponseClient<T_ServiceRequest, T_ServiceDelivery, T_Result>> logger)
		: base(httpClient, logger)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));
		ArgumentNullException.ThrowIfNull(config.Value.Endpoint, nameof(config.Value.Endpoint));

		_endpointAddress = config.Value.Endpoint;
		_logResponse = config.Value.LogResponse;
	}

	protected async Task<IEnumerable<T_Result>> RequestData(RequestResponseRequest<T_ServiceRequest> request, CancellationToken cancellationToken)
	{
		// convert to xml
		var xml = await SerializeObject(request).ConfigureAwait(false);
		// make the request
		var response = await PostRequest(_endpointAddress, xml, cancellationToken).ConfigureAwait(false);
		// convert the request

		if (_logResponse)
		{
			var responseXml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			_logger.LogDebug("{method} got response XML: {xml}", nameof(RequestData), responseXml);
		}

		var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
		var result = await DeserializeObject<SiriResponse<T_ServiceDelivery>>(contentStream).ConfigureAwait(false);

		return ConvertResponse(result);
	}
}
