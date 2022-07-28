using System;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Posts {
	public record PostCreateCommand : PostCreateMsg, IRequest {

		public PostCreateCommand(Guid id, PostDTO item) {
			this.Id = id;
			this.Item = item;
		}
	}

	public class PostCreateCommandValidator : AbstractValidator<PostCreateCommand> {

		public PostCreateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).Empty();
			//RuleFor(n => n.Item.OrderNumber).GreaterThan(0);
			//RuleFor(n => n.Item.Description).NotEmpty();
			//RuleFor(n => n.Item.IsDone).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}

	}

	internal class PostCreateCommandHandler : AbstractHandler<PostCreateCommand, PostCreateMsg, PostCreateResponse> {

		public PostCreateCommandHandler(IClient<PostCreateMsg, PostCreateResponse> client, ICacheService cache) : base(client, cache) { }

		protected override string CacheKey => $"board_{111}";
	}
}
