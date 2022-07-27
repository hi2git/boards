﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Domain.Contracts;

using MediatR;

namespace Boards.Front.Application.Commands {

	internal abstract class AbstractHandler<TRequest, TMsg, TResponse> : IRequestHandler<TRequest>
		where TRequest : IRequest
		where TMsg : AbstractMsg
		where TResponse : AbstractResponse {
		private readonly IClient<TMsg, TResponse> _client;

		public AbstractHandler(IClient<TMsg, TResponse> client) => _client = client;

		public async Task<Unit> Handle(TRequest request, CancellationToken token) {
			var msg = this.GetMsg(request);
			var response = await _client.Send(msg, token);
			await this.HandleResponse(response, request, token);
			return Unit.Value;
		}

		protected virtual Task HandleResponse(TResponse response, TRequest request, CancellationToken token) => Task.CompletedTask;

		protected virtual TMsg GetMsg(TRequest request) => request is TMsg msg
			? msg
			: throw new InvalidOperationException($"Couldn't get msg for {request.GetType().Name}");

	}

}