using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Commands;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserCreateConsumer : AbstractConsumer<UserCreateMsg, UserCreateResponse> {
		public UserCreateConsumer(IMediator mediator, IEventRepo eventRepo) : base(mediator, eventRepo) { }

		protected override Task Handle(UserCreateMsg item, CancellationToken token) => this.Mediator.Send(new UserCreateCommand(item), token);
	}
}
