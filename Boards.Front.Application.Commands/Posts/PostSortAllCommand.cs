using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;


using MediatR;

namespace Boards.Front.Application.Commands.Posts {
	public record PostSortAllCommand : PostSortAllMsg, IRequest {

		public PostSortAllCommand(Guid id, IEnumerable<PostDTO> items = null) {
			this.Id = id;
			this.Items = items;
		}

	}

	public class PostSortAllCommandValidator : AbstractValidator<PostSortAllCommand> {

		public PostSortAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			//RuleFor(n => n.Items).NotEmpty();
		}

	}

	internal class PostSortAllCommandHandler : AbstractHandler<PostSortAllCommand, PostSortAllMsg, PostSortedResponse> {

		public PostSortAllCommandHandler(IClient<PostSortAllMsg, PostSortedResponse> client, ICacheService cache) : base(client, cache) { }

		protected override string CacheKey(PostSortAllCommand request) => $"board_{request.Id}";
	}
}
