using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Commands;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserDeleteConsumer : AbstractConsumer<UserDeleteMsg, UserDeleteResponse> {
		public UserDeleteConsumer(IMediator mediator, IEventRepo eventRepo) : base(mediator, eventRepo) { }

		protected override Task Handle(UserDeleteMsg item, CancellationToken token) => this.Mediator.Send(new UserDeleteCommand(item), token);
	}
}
