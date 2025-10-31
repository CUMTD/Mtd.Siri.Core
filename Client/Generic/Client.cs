using Microsoft.Extensions.Logging;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Stopwatch.Core.Entities.Realtime;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mtd.Siri.Core.Client.Generic;

public abstract class Client<TServiceDelivery, TResult>
	where TServiceDelivery : ServiceDelivery
	where TResult : IRealtimeData
{
	protected readonly HttpClient _httpClient;

	protected readonly ILogger<Client<TServiceDelivery, TResult>> _logger;

	protected Client(HttpClient httpClient, ILogger<Client<TServiceDelivery, TResult>> logger)
	{
		ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
		ArgumentNullException.ThrowIfNull(logger, nameof(logger));

		_httpClient = httpClient;
		_logger = logger;
	}

	#region Protected Methods

	protected async Task<HttpResponseMessage> PostRequest(string endpoint, string requestData, CancellationToken cancellationToken)
	{
		var content = new StringContent(requestData, Encoding.UTF8, "text/xml");

		HttpResponseMessage response;
		try
		{
			response = await _httpClient
				.PostAsync(endpoint, content, cancellationToken)
				.ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to post to {endpoint}", endpoint);
			throw;
		}

		if (!response.IsSuccessStatusCode)
		{
			_logger.LogTrace("Response was {status}: {code}", "unsuccessful", response.StatusCode);
			throw new Exception($"{response.StatusCode}: {response.ReasonPhrase}");
		}

		_logger.LogTrace("Response was {status}: {code}", "successful", response.StatusCode);
		return response;
	}

	protected Task<string> SerializeObject<T>(T toSerialize) where T : class
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

	protected Task<T> DeserializeObject<T>(Stream objectStream) where T : class
	{
		var serializer = new XmlSerializer(typeof(T));
		var obj = serializer.Deserialize(objectStream) as T ?? throw new SerializationException($"Could not deserialize type \"{typeof(T).Name}\"");
		_logger.LogTrace("Deserialized object {type} from xml", typeof(T).Name);
		return Task.FromResult(obj);
	}

	#endregion Protected Methods

	#region Abstract Methods

	protected abstract IEnumerable<TResult> ConvertResponse(SiriResponse<TServiceDelivery> siriResponse);

	#endregion Abstract Methods
}
