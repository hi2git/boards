using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardItemUpdateCommand : IRequest {

		public BoardItemUpdateCommand(BoardItemDTO item) => this.Item = item;

		public BoardItemDTO Item { get; }
	}

	public class BoardItemUpdateCommandValidator : AbstractValidator<BoardItemUpdateCommand> {

		public BoardItemUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			//RuleFor(n => n.Item.OrderNumber).NotEmpty();
		}
	}

	internal class BoardItemUpdateCommandHandler : IRequestHandler<BoardItemUpdateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;

		public BoardItemUpdateCommandHandler(IUnitOfWork unitOfWork, IBoardItemRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Unit> Handle(BoardItemUpdateCommand request, CancellationToken token) {// TODO: check user before modify
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(item.Id.Value, token);
			entity = this.Map(item, entity);
			await _repo.Update(entity);
			await _unitOfWork.Commit();

			return Unit.Value;
		}

		private BoardItem Map(BoardItemDTO dto, BoardItem entity) {
			entity.IsDone = dto.IsDone;
			entity.Description = dto.Description;
			return entity;
		}

	}
}
