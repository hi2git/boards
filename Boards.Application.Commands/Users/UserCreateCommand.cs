using System;
using System.Linq;

using Board.Domain.DTO.Users;

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

		public UserCreateCommandHandler(IClient<UserCreateMsg, UserCreateResponse> client) : base(client) { }

	}
}
