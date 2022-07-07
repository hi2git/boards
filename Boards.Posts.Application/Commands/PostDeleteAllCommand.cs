using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Boards.Commons.Application.Utils;
using Boards.Domain.Contracts.Boards;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostDeleteAllCommand : BoardDeletedEvent, IRequest {

		public PostDeleteAllCommand(Guid id) : base(id) { }

	}

	public class PostDeleteAllCommandValidator : AbstractValidator<PostDeleteAllCommand> {

		public PostDeleteAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostDeleteAllCommandHandler : IRequestHandler<PostDeleteAllCommand> {
		private readonly IPostRepo _repo;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPublishEndpoint _publish;

		public PostDeleteAllCommandHandler(IPostRepo repo, IUnitOfWork unitOfWork, IPublishEndpoint publish) {
			_repo = repo;
			_unitOfWork = unitOfWork;
			_publish = publish;
		}

		public async Task<Unit> Handle(PostDeleteAllCommand request, CancellationToken token) {
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var items = await _repo.GetAll(id, token);

			await items.ForEachAsync(_repo.Delete);
			await _unitOfWork.Commit(() => items.WhenAll(n => _publish.Publish<PostDeletedEvent>(new(n.Id))));

			return Unit.Value;
		}
	}

}
