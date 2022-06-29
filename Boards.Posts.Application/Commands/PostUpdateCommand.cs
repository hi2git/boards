using System;

using Board.Domain.DTO.Posts;
using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostUpdateCommand : PostUpdateMsg, IRequest {

		public PostUpdateCommand(PostUpdateMsg msg) => this.Item = msg.Item;

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
		private readonly IBoardItemRepo _repo;

		public PostUpdateCommandHandler(IUnitOfWork unitOfWork, IBoardItemRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Unit> Handle(PostUpdateCommand request, CancellationToken token) {// TODO: check user before modify
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(item.Id.Value, token);
			entity = this.Map(item, entity);
			await _repo.Update(entity);
			await _unitOfWork.Commit();

			return Unit.Value;
		}

		private BoardItem Map(PostDTO dto, BoardItem entity) {
			entity.IsDone = dto.IsDone;
			entity.Description = dto.Description;
			return entity;
		}

	}
}
