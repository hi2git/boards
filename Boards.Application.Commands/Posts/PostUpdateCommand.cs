using System;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public record PostUpdateCommand : PostUpdateMsg, IRequest {

		public PostUpdateCommand(PostDTO item) => this.Item = item;

	}

	public class PostUpdateCommandValidator : AbstractValidator<PostUpdateCommand> {

		public PostUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			//RuleFor(n => n.Item.OrderNumber).NotEmpty();
		}
	}

	internal class PostUpdateCommandHandler : AbstractHandler<PostUpdateCommand, PostUpdateMsg, PostUpdateResponse> {

		public PostUpdateCommandHandler(IRequestClient<PostUpdateMsg> client) : base(client) { } 

	}
}
