using System;

using Board.Domain.Repos;

using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostUpdateCommand : PostUpdateMsg, IRequest {

		public PostUpdateCommand(PostUpdateMsg msg) : base(msg.Id) => this.Item = msg.Item;

	}

	public class PostUpdateCommandValidator : AbstractValidator<PostUpdateCommand> {

		public PostUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			//RuleFor(n => n.Item.OrderNumber).NotEmpty();
		}
	}

	internal class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPostRepo _repo;
		private readonly ICacheService _cache;

		public PostUpdateCommandHandler(IUnitOfWork unitOfWork, IPostRepo repo, ICacheService cache) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_cache = cache;
		}

		public async Task<Unit> Handle(PostUpdateCommand request, CancellationToken token) {// TODO: check user before modify
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(item.Id.Value, token);
			entity = this.Map(item, entity);
			await _repo.Update(entity);
			await _unitOfWork.Commit(); // TODO: save Intergration event

			await _cache.RemoveBoard(request.Id);

			return Unit.Value;
		}

		private Post Map(PostDTO dto, Post entity) {
			entity.IsDone = dto.IsDone;
			entity.Description = dto.Description;
			return entity;
		}

	}
}
