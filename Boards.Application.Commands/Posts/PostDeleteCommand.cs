using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public record PostDeleteCommand : PostDeleteMsg, IRequest {

		public PostDeleteCommand(Guid boardId, Guid id) {
			this.BoardId = boardId;
			this.Id = id;
		}
	}

	public class PostDeleteCommandValidator : AbstractValidator<PostDeleteCommand> {

		public PostDeleteCommandValidator() {
			RuleFor(n => n.BoardId).NotEmpty();
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostDeleteCommandHandler : AbstractHandler<PostDeleteCommand, PostDeleteMsg, PostDeleteResponse> {

		public PostDeleteCommandHandler(IRequestClient<PostDeleteMsg> client) : base(client) { } 

	}
}
