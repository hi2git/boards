using System;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Posts {
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

	internal class PostDeleteCommandHandler : AbstractPostCommandHandler<PostDeleteCommand, PostDeleteMsg, PostDeleteResponse> {
		public PostDeleteCommandHandler(IClient<PostDeleteMsg, PostDeleteResponse> client, ICacheService cache) : base(client, cache) { }

	}
}
