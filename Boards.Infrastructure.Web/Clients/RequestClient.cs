using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Application;
using Boards.Domain.Contracts;

using MassTransit;

namespace Boards.Infrastructure.Web.Clients {
	internal class RequestClient<TMsg, TResponse> : IClient<TMsg, TResponse> where TMsg : AbstractMsg where TResponse : AbstractResponse {
		private readonly IRequestClient<TMsg> _client;

		public RequestClient(IRequestClient<TMsg> client) => _client = client;

		// /// <inheritdoc/>
		//public Task<TResponse> Get(TMsg msg, CancellationToken token) {
		//	var response = await _client.GetResponse<PostGetAllResponse>(request, token);
		//	var item = response.Message;
		//	return !item.IsError 
		//		? item.Result
		//		: throw new CommandException(item.Message);
		//}

		/// <inheritdoc/>
		public async Task Send(TMsg msg, CancellationToken token) {
			var response = await _client.GetResponse<TResponse>(msg, token);
			var item = response.Message;
			if (item.IsError)
				throw new CommandException(item.Message);
		}

	}
}
