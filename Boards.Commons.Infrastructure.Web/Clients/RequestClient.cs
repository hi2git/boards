using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Domain.Contracts;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace Boards.Infrastructure.Web.Clients {
	internal class RequestClient<TMsg, TResponse> : IClient<TMsg, TResponse> where TMsg : AbstractMsg where TResponse : AbstractResponse {
		private readonly IRequestClient<TMsg> _client;
		private readonly ILogger _log;

		public RequestClient(IRequestClient<TMsg> client, ILogger<RequestClient<TMsg, TResponse>> log) {
			_client = client;
			_log = log;
		}

		/// <inheritdoc/>
		public async Task<TResponse> Send(TMsg msg, CancellationToken token) {
			var response = await _client.GetResponse<TResponse>(msg, token);
			var item = response.Message;
			if (item.IsError)
				throw new CommandException(item.Message);

			_log.LogDebug("{Action:l} {Type:l} ... {Result:l}", "Publishing", typeof(TMsg).Name, "OK");

			return item;
		}

	}
}
