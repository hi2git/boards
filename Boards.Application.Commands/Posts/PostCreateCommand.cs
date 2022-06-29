﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
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

	internal class PostCreateCommandHandler : AbstractHandler, IRequestHandler<PostCreateCommand> {
		private readonly IRequestClient<PostCreateMsg> _client;

		public PostCreateCommandHandler(IRequestClient<PostCreateMsg> client) => _client = client;

		public async Task<Unit> Handle(PostCreateCommand request, CancellationToken token) {
			var response = await _client.GetResponse<PostCreateResponse>(request, token);
			return ThrowIfError(response.Message);
		}
	}
}