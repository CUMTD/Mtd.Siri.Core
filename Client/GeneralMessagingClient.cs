using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mtd.Siri.Core.Client.Generic;
using Mtd.Siri.Core.Serialization.Request.GeneralMessaging;
using Mtd.Siri.Core.Serialization.Request.RequestRoot;
using Mtd.Siri.Core.Serialization.Response;
using Mtd.Siri.Core.Serialization.Response.GeneralMessaging;
using Mtd.Siri.Core.Serialization.Response.ServiceDeliveries;
using Microsoft.Extensions.Logging;
using Message = Mtd.Stopwatch.Core.Entities.Realtime.Message;

namespace Mtd.Siri.Core.Client
{
	public sealed class GeneralMessagingClient : RequestResponseClient<GeneralMessagingServiceRequest, GeneralMessageServiceDelivery, Message>
	{
		public GeneralMessagingClient(GeneralMessagingClientConfig config, HttpClient httpClient, ILogger<GeneralMessagingClient> logger) : base(config, httpClient, logger)
		{
		}

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
								?.Messages ?? Array.Empty<GeneralMessage>())
				.Select(gm => gm.Content.Message);

			return messages
				.Select(ToMessage);
		}

		private static Message ToMessage(Serialization.Response.GeneralMessaging.Message message) => new()
		{
			Id = message.Id,
			StartTime = CombineDateAndTime(message.StartDay, message.StartTime),
			EndTime = CombineDateAndTime(message.EndDay, message.EndTime),
			DisplayId = message.DisplayId,
			BlockRealtime = message.BlockRealtime,
			StopIds = message.StopIds,
			Text = ConvertLinesToLine(message.Text.Lines)
		};

		private static string ConvertLinesToLine(IEnumerable<string> lines) =>
				lines
			.Aggregate(
					new StringBuilder(),
					(sb, s) => sb.AppendFormat(" / {0}", s),
					sb => sb.ToString().Trim('/', ' '));

		private static DateTime CombineDateAndTime(DateTime date, DateTime time) =>
			new(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond, DateTimeKind.Local);
	}
}
