using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public abstract class AbstractConsumer<TMsg, TResponse> : IConsumer<TMsg> where TMsg : class where TResponse : IResponse, new() {

		public AbstractConsumer(IMediator mediator) => this.Mediator = mediator;

		public async Task Consume(ConsumeContext<TMsg> context) {
			var msg = await this.TryConsume(context.Message, context.CancellationToken);
			var response = new TResponse() { Message = msg ?? string.Empty };
			await context.RespondAsync(response);
		}

		#region Protected

		protected IMediator Mediator { get; }

		protected abstract Task Handle(TMsg item, CancellationToken token);

		#endregion

		#region Private

		private async Task<string?> TryConsume(TMsg item, CancellationToken token) {
			string? msg = null;
			try {
				await this.Handle(item, token);
			}
			catch (Exception e) {
				msg = e.Message; // TODO: log msg
			}
			return msg;
		}

		#endregion
	}
}
