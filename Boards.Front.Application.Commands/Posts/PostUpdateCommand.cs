﻿using System;

using Boards.Commons.Application;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Posts {
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

		public PostUpdateCommandHandler(IClient<PostUpdateMsg, PostUpdateResponse> client) : base(client) { }

	}
}