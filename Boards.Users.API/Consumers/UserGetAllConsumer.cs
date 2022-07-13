using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Queries;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserGetAllConsumer : AbstractGetConsumer<UserGetAllMsg, UserGetAllResponse> {

		public UserGetAllConsumer(IMediator mediator) : base(mediator) { }

		protected override async Task<UserGetAllResponse> Handle(UserGetAllMsg item, CancellationToken token) {
			var items = await this.Mediator.Send(new UserGetAllQuery(), token);
			return new() { Items = items };
		}
	}
}
