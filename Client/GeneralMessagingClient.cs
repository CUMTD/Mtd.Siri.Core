using Microsoft.Extensions.Logging;
using Mtd.Siri.Core.Client.Generic;
using Mtd.Siri.Core.Serialization.Request.GeneralMessaging;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;
using System.Text;
using Message = Mtd.Stopwatch.Core.Entities.Realtime.Message;

namespace Mtd.Siri.Core.Client;

public sealed class GeneralMessagingClient(GeneralMessagingClientConfig config, HttpClient httpClient, ILogger<GeneralMessagingClient> logger)
	: RequestResponseClient<GeneralMessagingServiceRequest, GeneralMessageServiceDelivery, Message>(config, httpClient, logger)
{
	public async Task<IEnumerable<Message>> GetAllMessages(CancellationToken cancellationToken)
	{
		var request = new RequestResponseRequest<GeneralMessagingServiceRequest>();
		return await RequestData(request, cancellationToken).ConfigureAwait(false);
	}

	protected override IEnumerable<Message> ConvertResponse(SiriResponse<GeneralMessageServiceDelivery> siriResponse)
	{
		var messages = (siriResponse
							?.ServiceDelivery
							?.Result
							?.Messages ?? [])
			.Select(gm => gm.Content.Message);

		return messages
			.Select(ToMessage)
			.Where(m => m != null)
			.Select(m => m!);
	}

	private static Message? ToMessage(Serialization.Response.GeneralMessaging.Message? message)
	{
		if (message == null || message.Id == null || message.StartDay == null || message.StartTime == null || message.StopIds == null || message?.Text?.Lines == null || message.Text.Lines.Length == 0)
		{
			return null;
		}

		return new()

		{
			Id = message.Id,
			StartTime = CombineDateAndTime(message.StartDay, message.StartTime) ?? throw new InvalidOperationException("StartTime can't be null. This should never be hit."),
			EndTime = CombineDateAndTime(message.EndDay, message.EndTime),
			DisplayId = message.DisplayId,
			BlockRealtime = message.BlockRealtime,
			StopIds = message.StopIds,
			Text = ConvertLinesToLine(message.Text.Lines)
		};
	}

	private static string ConvertLinesToLine(IEnumerable<string> lines) =>
			lines
		.Aggregate(
				new StringBuilder(),
				(sb, s) => sb.AppendFormat(" / {0}", s),
				sb => sb.ToString().Trim('/', ' '));

	private static DateTimeOffset? CombineDateAndTime(DateTime? date, DateTime? time)
	{
		if (date == null || time == null)
		{
			return null;
		}

		return new(
		date.Value.Year, date.Value.Month, date.Value.Day,
		time.Value.Hour, time.Value.Minute, time.Value.Second, time.Value.Millisecond,
		TimeZoneInfo.Local.GetUtcOffset(date.Value)
	);
	}
}
