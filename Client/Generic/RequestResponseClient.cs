using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mtd.Siri.Core.Serialization.Request;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Stopwatch.Core.Entities.Realtime;
using Microsoft.Extensions.Logging;

namespace Mtd.Siri.Core.Client.Generic
{
	public abstract class RequestResponseClient<TServiceRequest, TServiceDelivery, TResult> : Client<TServiceDelivery, TResult>
		where TServiceRequest : ServiceRequest, new()
		where TServiceDelivery : RequestResponseServiceDelivery
		where TResult : IRealtimeData
	{
		private readonly string _endpointAddress;
		private readonly bool _logResponse;

		protected RequestResponseClient(RequestResponseClientConfig config, HttpClient httpClient, ILogger<RequestResponseClient<TServiceRequest, TServiceDelivery, TResult>> logger)
			: base(httpClient, logger)
		{
			_endpointAddress = config.Endpoint;
			_logResponse = config.LogResponse;
		}

		protected async Task<IEnumerable<TResult>> RequestData(RequestResponseRequest<TServiceRequest> request, CancellationToken cancellationToken)
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
			var result = await DeserializeObject<SiriResponse<TServiceDelivery>>(contentStream).ConfigureAwait(false);

			return ConvertResponse(result);
		}
	}
}
