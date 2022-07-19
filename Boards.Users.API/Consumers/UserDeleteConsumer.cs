using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Commands;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserDeleteConsumer : AbstractCommandConsumer<UserDeleteMsg, UserDeleteResponse> {
		public UserDeleteConsumer(IMediator mediator, IEventRepo eventRepo, ILogger<UserDeleteConsumer> log) : base(mediator, eventRepo, log) { }

		protected override Task Handle(UserDeleteMsg item, CancellationToken token) => this.Mediator.Send(new UserDeleteCommand(item), token);
	}
}
