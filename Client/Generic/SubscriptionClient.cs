using Microsoft.Extensions.Logging;
using Mtd.Siri.Core.Config;
using Mtd.Siri.Core.Serialization.Request;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Stopwatch.Core.Entities.Realtime;

namespace Mtd.Siri.Core.Client.Generic;

public abstract class SubscriptionClient<TSubscriptionRequest, TServiceDelivery, TResult> : Client<TServiceDelivery, TResult>, ISubscriptionClient<TSubscriptionRequest, TResult> where TSubscriptionRequest : SubscriptionRequest, new()
	where TServiceDelivery : ServiceDelivery
	where TResult : IRealtimeData
{
	protected readonly Uri _subscribeAddress;
	protected readonly Uri _dataSuplyEndpointAddress;
	protected readonly string _subscriberRef;

	protected SubscriptionClient(SubscriptionClientOptions config, HttpClient httpClient, ILogger<SubscriptionClient<TSubscriptionRequest, TServiceDelivery, TResult>> logger)
		: base(httpClient, logger)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));
		ArgumentNullException.ThrowIfNull(config.SubscribeAddress, nameof(config.SubscribeAddress));
		ArgumentNullException.ThrowIfNull(config.DataSuplyEndpointAddress, nameof(config.DataSuplyEndpointAddress));
		ArgumentException.ThrowIfNullOrWhiteSpace(config.SubscriberRef, nameof(config.SubscriberRef));

		_subscribeAddress = config.SubscribeAddress;
		_dataSuplyEndpointAddress = config.DataSuplyEndpointAddress;
		_subscriberRef = config.SubscriberRef;
	}

	protected virtual SubscriptionRequest<TSubscriptionRequest> CreateRequest()
	{
		// create request
		var subscriptionRequest = new SubscriptionRequest<TSubscriptionRequest>();
		subscriptionRequest.Request.SetRequestor(_subscriberRef);
		return subscriptionRequest;
	}

	public async Task<SubscriptionRequest<TSubscriptionRequest>> SubscribeAsync(CancellationToken cancellationToken)
	{
		// create request
		var subscriptionRequest = CreateRequest();

		// convert to xml
		var xml = await SerializeObject(subscriptionRequest).ConfigureAwait(false);
		// make the request
		var response = await PostRequest(_subscribeAddress, xml, cancellationToken).ConfigureAwait(false);

		var responseXml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
		_logger.LogDebug("{method} got response XML: {xml}", nameof(SubscribeAsync), responseXml);

		return subscriptionRequest;
	}

	public async Task<IEnumerable<TResult>> GetDataReadyResultsAsync(CancellationToken cancellationToken)
	{
		var siriResponse = await MakeDataSuplyRequest(cancellationToken).ConfigureAwait(false);
		var result = ConvertResponse(siriResponse);
		return result;
	}

	private async Task<SiriResponse<TServiceDelivery>> MakeDataSuplyRequest(CancellationToken cancellationToken)
	{
		// create request
		var request = new DataSuplySiriRequest
		{
			Request =
			{
				Consumer = _subscriberRef
			}
		};
		// convert to xml
		var xml = await SerializeObject(request).ConfigureAwait(false);
		_logger.LogDebug("{method} serialized request to: {xml}", nameof(MakeDataSuplyRequest), xml);

		// make request and check for errors
		var response = await PostRequest(_dataSuplyEndpointAddress, xml, cancellationToken).ConfigureAwait(false);

		var responseXml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
		_logger.LogDebug("{method} got response XML: {xml}", nameof(MakeDataSuplyRequest), responseXml);

		//deserialize response
		var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
		return await DeserializeObject<SiriResponse<TServiceDelivery>>(responseStream).ConfigureAwait(false);
	}
}
