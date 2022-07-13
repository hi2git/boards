using Boards.Domain.Contracts.Auths;
using Boards.Users.Application.Queries;

using MassTransit;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class TokenGetConsumer : IConsumer<TokenGetMsg> {
		private readonly IMediator _mediator;

		public TokenGetConsumer(IMediator mediator) => _mediator = mediator;

		public async Task Consume(ConsumeContext<TokenGetMsg> context) {
			var msg = context.Message;
			try {
				var token = await _mediator.Send(new TokenGetQuery(msg), context.CancellationToken);
				await context.RespondAsync<TokenGetResponse>(new() { Token = token });
			}
			catch (Exception ex) { 
				await context.RespondAsync<TokenGetResponse>(new() { Message = ex.Message });

			}
		}
	}
}
