using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
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

	internal class PostSortAllCommandHandler : AbstractHandler, IRequestHandler<PostSortAllCommand> {
		private readonly IRequestClient<PostSortAllMsg> _client;

		public PostSortAllCommandHandler(IRequestClient<PostSortAllMsg> client) => _client = client;

		public async Task<Unit> Handle(PostSortAllCommand request, CancellationToken token) {
			var response = await _client.GetResponse<PostSortedResponse>(request, token);
			return ThrowIfError(response.Message);
		}
	}
}
