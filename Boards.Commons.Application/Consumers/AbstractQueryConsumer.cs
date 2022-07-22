using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Boards.Commons.Application.Consumers {
	public abstract class AbstractQueryConsumer<TMsg, TResponse> : IConsumer<TMsg> where TMsg : class where TResponse : IResponse, new() {
		private readonly ILogger _log;

		public AbstractQueryConsumer(IMediator mediator, ILogger log) {
			this.Mediator = mediator;
			_log = log;
		}

		public async Task Consume(ConsumeContext<TMsg> context) {
			//_log.LogDebug($"Consuming query {typeof(TMsg).Name} - {context.MessageId}...");
			var response = await this.TryConsume(context.Message, context.CancellationToken);
			await context.RespondAsync(response);
		}

		protected IMediator Mediator { get; }

		protected abstract Task<TResponse> Handle(TMsg item, CancellationToken token);


		private async Task<TResponse> TryConsume(TMsg item, CancellationToken token) {
			try {
				return await this.Handle(item, token);
			}
			catch (Exception e) {
				_log.LogError(e, "Unhandled error");
				return new TResponse() { Message = e.Message };
			}
		}

	}


}
