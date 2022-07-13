using Boards.Domain.Contracts.Auths;
using Boards.Users.Application;

using MassTransit;

namespace Boards.Users.API.Consumers {
	public class TokenGetConsumer : IConsumer<TokenGetMsg> {
		private readonly IAuthService _authSvc;

		public TokenGetConsumer(IAuthService authSvc) => _authSvc = authSvc;

		public async Task Consume(ConsumeContext<TokenGetMsg> context) {
			var msg = context.Message;
			try {
				var token = await _authSvc.Login(msg.Item, msg.Auth, context.CancellationToken);
				await context.RespondAsync<TokenGetResponse>(new() { Token = token });
			}
			catch (Exception ex) { 
				await context.RespondAsync<TokenGetResponse>(new() { Message = ex.Message });

			}
		}
	}
}
