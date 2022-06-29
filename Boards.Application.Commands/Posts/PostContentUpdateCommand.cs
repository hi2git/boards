using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public record PostContentUpdateCommand : PostContentUpdateMsg, IRequest {

		public PostContentUpdateCommand(PostDTO item) => this.Item = item;

	}

	public class PostContentUpdateCommandValidator : AbstractValidator<PostContentUpdateCommand> {

		public PostContentUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}
	}

	internal class PostContentUpdateCommandGandler : AbstractHandler<PostContentUpdateCommand, PostContentUpdateMsg, PostContentUpdateResponse> {

		public PostContentUpdateCommandGandler(IRequestClient<PostContentUpdateMsg> client) : base(client) { }

	}
}
