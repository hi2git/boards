using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Queries;

using MassTransit;

using MediatR;

namespace Boards.Users.API.Consumers {
	public class UserGetAllConsumer : IConsumer<UserGetAllMsg> {
		private readonly IMediator _mediator;

		public UserGetAllConsumer(IMediator mediator) => _mediator = mediator;

		public Task Consume(ConsumeContext<UserGetAllMsg> context) => _mediator.Send(new UserGetAllQuery(), context.CancellationToken);
	}
}
