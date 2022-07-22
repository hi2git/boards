using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Boards.Commons.Application.Consumers {
	public abstract class AbstractCommandConsumer<TMsg, TResponse> : IConsumer<TMsg> where TMsg : class where TResponse : IResponse, new() {
		private readonly IEventRepo _eventRepo;
		private readonly ILogger _log;

		public AbstractCommandConsumer(IMediator mediator, IEventRepo eventRepo, ILogger log) {
			this.Mediator = mediator;
			_eventRepo = eventRepo;
			_log = log;
		}

		public async Task Consume(ConsumeContext<TMsg> context) {
			//_log.LogDebug($"Consuming command {typeof(TMsg).Name} - {context.MessageId}...");
			await this.HandleEvent(context.MessageId);
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

		private async Task HandleEvent(Guid? messageId) {
			var name = typeof(TMsg).Name;
			var id = messageId ?? throw new InvalidOperationException($"No MessageId found for {name}");
			var @event = new IntegrationEvent(id, name, DateTime.Now); // TODO: use IDateService

			await _eventRepo.Create(@event);
		}

		#endregion
	}
}
