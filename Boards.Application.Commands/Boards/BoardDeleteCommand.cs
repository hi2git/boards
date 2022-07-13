using System;
using System.Linq;


using Boards.Commons.Application;
using Boards.Domain.Contracts.Boards;

using FluentValidation;


using MediatR;

namespace Boards.Application.Commands.Boards {
	
	public record BoardDeleteCommand : BoardDeleteMsg, IRequest {

		public BoardDeleteCommand(Guid id) : base(id) { }

	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : AbstractHandler<BoardDeleteCommand, BoardDeleteMsg, BoardDeleteResponse> {  

		public BoardDeleteCommandHandler(IClient<BoardDeleteMsg, BoardDeleteResponse> client) : base(client) { }

	}
}
