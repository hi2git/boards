using System;

using Board.Domain.Repos;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Images;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostCreateCommand : PostCreateMsg, IRequest {

		public PostCreateCommand(PostCreateMsg msg) : base(msg.Id) => this.Item = msg.Item;
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

	internal class PostCreateCommandHandler : IRequestHandler<PostCreateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPostRepo _repo;
		private readonly IClient<ImageUpdateMsg, ImageUpdateResponse> _client;
		private readonly ICacheService _cache;

		public PostCreateCommandHandler(IUnitOfWork unitOfWork, IPostRepo repo, IClient<ImageUpdateMsg, ImageUpdateResponse> client, ICacheService cache) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_client = client;
			_cache = cache;
		}

		public async Task<Unit> Handle(PostCreateCommand request, CancellationToken token) {
			var dto = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var item = new Post(Guid.NewGuid(), request.Id, dto.OrderNumber, dto.Description);

			await _repo.Create(item);
			await _unitOfWork.Commit();

			await _cache.RemoveBoard(request.Id);

			await _client.Send(new (item.Id, dto.Content), token);
			return Unit.Value;
		}
	}
}
