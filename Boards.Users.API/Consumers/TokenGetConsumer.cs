using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Auths;
using Boards.Users.Application.Queries;

using MediatR;

namespace Boards.Users.API.Consumers {

	public class TokenGetConsumer : AbstractGetConsumer<TokenGetMsg, TokenGetResponse> {
		public TokenGetConsumer(IMediator mediator) : base(mediator) { }

		protected override async Task<TokenGetResponse> Handle(TokenGetMsg item, CancellationToken token) {
			var jwt = await this.Mediator.Send(new TokenGetQuery(item), token);
			return new() { Token = jwt };
		}
	}
}
