using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Commands;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserUpdateConsumer : AbstractConsumer<UserUpdateMsg, UserUpdateResponse> {
		public UserUpdateConsumer(IMediator mediator, IEventRepo eventRepo) : base(mediator, eventRepo) { }

		protected override Task Handle(UserUpdateMsg item, CancellationToken token) => this.Mediator.Send(new UserUpdateCommand(item), token);
	}
}
