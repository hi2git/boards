using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Auths;
using Boards.Users.Application.Queries;

using MediatR;

namespace Boards.Users.API.Consumers {

	public class TokenGetConsumer : AbstractQueryConsumer<TokenGetMsg, TokenGetResponse> {
		public TokenGetConsumer(IMediator mediator, ILogger<TokenGetConsumer> log) : base(mediator, log) { }

		protected override async Task<TokenGetResponse> Handle(TokenGetMsg item, CancellationToken token) {
			var jwt = await this.Mediator.Send(new TokenGetQuery(item), token);
			return new() { Token = jwt };
		}
	}
}
