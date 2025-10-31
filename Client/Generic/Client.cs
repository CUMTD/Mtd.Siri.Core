using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Stopwatch.Core.Entities.Realtime;
using Microsoft.Extensions.Logging;

namespace Mtd.Siri.Core.Client.Generic
{
	public abstract class Client<TServiceDelivery, TResult>
		where TServiceDelivery : ServiceDelivery
		where TResult : IRealtimeData
	{
		protected readonly HttpClient _httpClient;

		protected readonly ILogger<Client<TServiceDelivery, TResult>> _logger;

		protected Client(HttpClient httpClient, ILogger<Client<TServiceDelivery, TResult>> logger)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		#region Protected Methods

		protected async Task<HttpResponseMessage> PostRequest(string endpoint, string requestData, CancellationToken cancellationToken)
		{
			var content = new StringContent(requestData, Encoding.UTF8, "text/xml");

			HttpResponseMessage response = default;
			try
			{
				response = await _httpClient.PostAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Failed to post to {endpoint}", endpoint);
			}

			if (response == default)
			{
				_logger.LogWarning("SIRI Response was null");
				throw new Exception("Response was null");
			}

			if (!response.IsSuccessStatusCode)
			{
				_logger.LogTrace("Response was {status}: {code}", "unsuccessful", response.StatusCode);
				throw new Exception($"{response.StatusCode}: {response.ReasonPhrase}");
			}

			_logger.LogTrace("Response was {status}: {code}", "successful", response.StatusCode);
			return response;
		}

		protected Task<string> SerializeObject<T>(T toSerialize)
		{
			var type = toSerialize.GetType();
			var serializer = new XmlSerializer(type);

			var xmlWriterSettings = new XmlWriterSettings
			{
				Encoding = new UnicodeEncoding(false, false),
				Indent = false,
				OmitXmlDeclaration = false
			};

			var resultStringBuilder = new StringBuilder();
			using (var stringWriter = new StringWriter(resultStringBuilder))
			{
				using var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);
				serializer.Serialize(xmlWriter, toSerialize);
			}

			var serialized = resultStringBuilder.ToString();
			_logger.LogTrace("Serialized object {type} to {xml}", typeof(T).Name, serialized);

			return Task.FromResult(serialized);
		}

		protected Task<T> DeserializeObject<T>(Stream objectStream)
		{
			var serializer = new XmlSerializer(typeof(T));
			var obj = (T)serializer.Deserialize(objectStream);
			_logger.LogTrace("Deserialized object {type} from xml", typeof(T).Name);
			return Task.FromResult(obj);
		}

		#endregion Protected Methods

		#region Abstract Methods

		protected abstract IEnumerable<TResult> ConvertResponse(SiriResponse<TServiceDelivery> siriResponse);

		#endregion Abstract Methods
	}
}
