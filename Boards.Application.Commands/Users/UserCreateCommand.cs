using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;

using Boards.Application.Commands.Auths;
using Boards.Commons.Application;
using Boards.Domain.Contracts.Users;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Users {
	public record UserCreateCommand : UserCreateMsg, IRequest {

		public UserCreateCommand(LoginDTO item) : base(item) { }

	}

	public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand> {

		public UserCreateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty().MaximumLength(50);
			RuleFor(n => n.Item.Email).NotEmpty().MaximumLength(50).EmailAddress();
			RuleFor(n => n.Item.Login).NotEmpty().MaximumLength(50);
		}

	}

	internal class UserCreateCommandHandler : AbstractHandler<UserCreateCommand, UserCreateMsg, UserCreateResponse> {
		private readonly IMediator _mediator;

		public UserCreateCommandHandler(IClient<UserCreateMsg, UserCreateResponse> client, IMediator mediator) : base(client) => _mediator = mediator;

		protected override Task HandleResponse(UserCreateResponse response, UserCreateCommand request, CancellationToken token) => _mediator.Send(new LoginCommand(request.Item), token);

	}
}
