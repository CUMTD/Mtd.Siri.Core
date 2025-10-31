using System;
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
	public abstract class SubscriptionClient<TSubscriptionRequest, TServiceDelivery, TResult> : Client<TServiceDelivery, TResult>
		where TSubscriptionRequest : SubscriptionRequest, new()
		where TServiceDelivery : ServiceDelivery
		where TResult : IRealtimeData
	{
		protected readonly string _subscribeAddress;
		protected readonly string _dataSuplyEndpointAddress;
		protected readonly string _subscriberRef;

		protected SubscriptionClient(SubscriptionClientConfig config, HttpClient httpClient, ILogger<SubscriptionClient<TSubscriptionRequest, TServiceDelivery, TResult>> logger)
			: base(httpClient, logger)
		{
			if (string.IsNullOrEmpty(config.SubscribeAddress))
			{
				throw new ArgumentNullException(nameof(config), "No SubscriberAddress");
			}

			_subscribeAddress = config.SubscribeAddress;

			if (string.IsNullOrEmpty(config.DataSuplyEndpointAddress))
			{
				throw new ArgumentNullException(nameof(config), "No DataSuplyEndpointAddress");
			}

			_dataSuplyEndpointAddress = config.DataSuplyEndpointAddress;
			if (string.IsNullOrEmpty(config.SubscriberRef))
			{
				throw new ArgumentNullException(nameof(config), "No SubscriberRef");
			}

			_subscriberRef = config.SubscriberRef;
		}

		protected virtual SubscriptionRequest<TSubscriptionRequest> CreateRequest()
		{
			// create request
			var subscriptionRequest = new SubscriptionRequest<TSubscriptionRequest>();
			subscriptionRequest.Request.SetRequestor(_subscriberRef);
			return subscriptionRequest;
		}

		public async Task<SubscriptionRequest<TSubscriptionRequest>> Subscribe(CancellationToken cancellationToken)
		{
			// create request
			var subscriptionRequest = CreateRequest();

			// convert to xml
			var xml = await SerializeObject(subscriptionRequest).ConfigureAwait(false);
			// make the request
			var response = await PostRequest(_subscribeAddress, xml, cancellationToken).ConfigureAwait(false);

			var responseXml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			_logger.LogDebug("{method} got response XML: {xml}", nameof(Subscribe), responseXml);

			return subscriptionRequest;
		}

		public async Task<IEnumerable<TResult>> GetDataReadyResults(CancellationToken cancellationToken)
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
}
