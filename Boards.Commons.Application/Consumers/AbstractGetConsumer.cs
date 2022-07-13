using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

namespace Boards.Commons.Application.Consumers {
	public abstract class AbstractGetConsumer<TMsg, TResponse> : IConsumer<TMsg> where TMsg : class where TResponse : IResponse, new() {

		public AbstractGetConsumer(IMediator mediator) => this.Mediator = mediator;


		public async Task Consume(ConsumeContext<TMsg> context) {
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
				return new TResponse() { Message = e.Message };
			}
		}

	}


}
